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
        private readonly IRepository<InventoryItem> itemsRepository;
        private readonly CatalogClient catalogClient;
        public ItemsController(
            IRepository<InventoryItem> itemsRepository,
            CatalogClient catalogClient
        )
        {
            this.itemsRepository = itemsRepository;
            this.catalogClient = catalogClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                return BadRequest();
            }

            var catalogItems = await catalogClient.GetCatalogItemsAsync();
            var inventoryItemEntities = await itemsRepository.GetAllAsync(
                item => item.UserId == userId
            );

            var inventoryItemDtos = inventoryItemEntities.Select(
                inventoryItem => 
            {
                var catalogItem = catalogItems.Single(
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
            var inventoryItem = await itemsRepository.GetAsync(
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

                await itemsRepository.CreateAsync(inventoryItem);
            }
            else
            {
                inventoryItem.Quantity += grantItemDto.Quantity;
                await itemsRepository.UpdateAsync(inventoryItem);
            }

            return Ok();
        }
    }
}