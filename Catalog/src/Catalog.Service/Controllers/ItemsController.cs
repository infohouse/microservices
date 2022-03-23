using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Service.Dtos;
using Catalog.Service.Entities;
using Catalog.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        
        public readonly IRepository<Item> itemsrepository;

        public ItemsController(IRepository<Item> itemsrepository)
        {
            this.itemsrepository = itemsrepository; 
        }
        
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await itemsrepository.GetAllAsync())
                        .Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item =  await itemsrepository.GetAsync(id);
            
            if(item == null)  return NotFound();

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createitemdto)
        {
            
            var item = new Item
            {
                Name = createitemdto.Name,
                Description = createitemdto.Description,
                Price = createitemdto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await itemsrepository.CreateAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync), new {id = item.Id}, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateitemdto)
        {
            
            var existeItem = await itemsrepository.GetAsync(id);
            
            if(existeItem == null) return NotFound();

            existeItem.Name = updateitemdto.Name;
            existeItem.Description = updateitemdto.Description;
            existeItem.Price = updateitemdto.Price;
            
            await itemsrepository.UpdateAsync(existeItem);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var existeItem = await itemsrepository.GetAsync(id);
            if(existeItem == null) return NotFound();

            await itemsrepository.RemoveAsync(existeItem.Id);
            
            return NoContent();           ;
            
        }


    }
}