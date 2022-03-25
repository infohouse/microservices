using System.Threading.Tasks;
using Common;
using Contracts;
using inventory.Entities;
using MassTransit;

namespace inventory.Consumers
{
    public class CatalogItemUpdatedConsumer : IConsumer<CatlogItemUpdated>
    {
        private readonly IRepository<CatalogItem> repository;
        
        public CatalogItemUpdatedConsumer(IRepository<CatalogItem> repository)
        {
            this.repository = repository;
        }
        
        public async Task Consume(ConsumeContext<CatlogItemUpdated> context)
        {
            var message = context.Message;
            var item = await repository.GetAsync(message.itemId);

            if(item == null) {

                item = new CatalogItem{
                    Id = message.itemId,
                    Name = message.Name,
                    Description = message.Description
                };

                await repository.UpdateAsync(item);
            }
            else{

                item.Name = message.Name;
                item.Description = message.Description;

                await repository.UpdateAsync(item);
            }

            

        }
    }

}