using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using inventory.Clients;
using inventory.Dtos;
using inventory.Entities;
using Microsoft.AspNetCore.Mvc;

namespace inventory
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<InventoryItem> inventoryItemsRepository;
        private readonly IRepository<CatalogItem> catalogitemsRepository;
        
        public ItemsController(
            IRepository<InventoryItem> inventoryItemsRepository,
            IRepository<CatalogItem> catalogitemsRepository
        )
        {
            this.inventoryItemsRepository = inventoryItemsRepository;
            this.catalogitemsRepository = catalogitemsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                return BadRequest();
            }

            //var catalogItems = await catalogClient.GetCatalogItemsAsync();
            
            var inventoryItemEntities = await inventoryItemsRepository.GetAllAsync(
                item => item.UserId == userId
            );

            var itemIds = inventoryItemEntities.Select(item => item.CatalogItemId);

            var catalogItemEntities = await catalogitemsRepository.GetAllAsync(item => itemIds.Contains(item.Id));    

            var inventoryItemDtos = inventoryItemEntities.Select(
                inventoryItem => 
            {
                var catalogItem = catalogItemEntities.Single(
                    catalogItem => catalogItem.Id == inventoryItem.CatalogItemId
                );

                return inventoryItem.AsDto(
                    catalogItem.Name,
                    catalogItem.Description
                );   
            });

            return Ok(inventoryItemDtos);
        } 

        [HttpPost]
        public async Task<ActionResult> PostAsync(GrantItemDto grantItemDto)
        {
            var inventoryItem = await inventoryItemsRepository.GetAsync(
                item => item.UserId == grantItemDto.UserId 
                && item.CatalogItemId == grantItemDto.CatalogItemId 
            );

            if(inventoryItem == null) 
            {
                inventoryItem = new InventoryItem
                {
                    CatalogItemId = grantItemDto.CatalogItemId,
                    UserId = grantItemDto.UserId,
                    Quantity = grantItemDto.Quantity,
                    AcquiredDate = DateTimeOffset.UtcNow
                };

                await inventoryItemsRepository.CreateAsync(inventoryItem);
            }
            else
            {
                inventoryItem.Quantity += grantItemDto.Quantity;
                await inventoryItemsRepository.UpdateAsync(inventoryItem);
            }

            return Ok();
        }
    }
}