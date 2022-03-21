using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;

namespace PlaformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient 
    {
        private readonly HttpClient _httpclient;
        private IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpclient, IConfiguration configuration)
        {
            _httpclient = httpclient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
                        
            var httpContent = new StringContent(
                JsonSerializer.Serialize(plat),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpclient.PostAsync(
                "http://localhost:6000/api/c/platforms",
                httpContent
            );
            
            if(response.IsSuccessStatusCode)
            {
                var saida = " --> Post com sucesso!";
                Console.WriteLine(saida);
            }
            else
            {
                var saida = "--> Post sem sucesso!";
                Console.WriteLine(saida);
            }
        }
    }
}