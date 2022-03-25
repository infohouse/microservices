using System.Threading.Tasks;
using Common;
using Contracts;
using inventory.Entities;
using MassTransit;

namespace inventory.Consumers
{
    public class CatalogItemDeletedConsumer : IConsumer<CatlogItemDeleted>
    {
        private readonly IRepository<CatalogItem> repository;
        
        public CatalogItemDeletedConsumer(IRepository<CatalogItem> repository)
        {
            this.repository = repository;
        }
        
        public async Task Consume(ConsumeContext<CatlogItemDeleted> context)
        {
            var message = context.Message;
            var item = await repository.GetAsync(message.itemId);

            if(item != null) return;

            await repository.RemoveAsync(message.itemId);

        }
    }

}