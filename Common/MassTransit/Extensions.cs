using System;
using System.Reflection;
using Common.settings;
using Common.Settings;
using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.MassTransit
{
    public static class Extensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        {
            
           services.AddMassTransit(x => {
                
                x.AddConsumers(Assembly.GetEntryAssembly());
                
                x.UsingRabbitMq((context, configurator) => {
                    var configuration = context.GetService<IConfiguration>();
                    var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                    var rabbitMQSettings = configuration.GetSection(
                        nameof(RabbitMQSettings)
                    ).Get<RabbitMQSettings>();

                    configurator.Host(rabbitMQSettings.Host);
                    
                    configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(
                        serviceSettings.ServiceName, false
                    ));

                    configurator.UseMessageRetry(retryConfigurator => {
                        retryConfigurator.Interval(3, TimeSpan.FromSeconds(5));
                    });

                });                
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}