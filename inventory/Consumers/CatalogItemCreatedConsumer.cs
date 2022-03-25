using System.Threading.Tasks;
using Common;
using Contracts;
using inventory.Entities;
using MassTransit;

namespace inventory.Consumers
{
    public class CatalogItemCreatedConsumer : IConsumer<CatlogItemCreated>
    {
        private readonly IRepository<CatalogItem> repository;
        
        public CatalogItemCreatedConsumer(IRepository<CatalogItem> repository)
        {
            this.repository = repository;
        }
        
        public async Task Consume(ConsumeContext<CatlogItemCreated> context)
        {
            var message = context.Message;
            var item = await repository.GetAsync(message.itemId);

            if(item != null) return;

            item = new CatalogItem{
                Id = message.itemId,
                Name = message.Name,
                Description = message.Description
            };

            await repository.CreateAsync(item);

        }
    }

}