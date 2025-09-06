using Inventory_api.src.Application.Services;
using Inventory_api.src.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_api.WebAPI.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ItemResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(new {success = true, data = items});
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ItemResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetItemById(int Id)
        {
            var item = await _itemService.GetItemByIdAsync(Id);
            return Ok(new {success = true, data=item});
        }

        [HttpGet]
        [Route("search")]
        [Authorize]
        [ProducesResponseType(typeof(ItemResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetItemByName(string name)
        {
            var item = await _itemService.SearchItemsByNameAsync(name);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(new {success = true, data = item});
        }


        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ItemResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateItem([FromBody] ItemCreateDto itemDto)
        {
            var createdItem = await _itemService.CreateNewItemAsync(itemDto);
            return CreatedAtAction(nameof(GetItemById), new {id = createdItem.ItemId}, new {success = true, data = createdItem});
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ItemResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] ItemCreateDto itemDto)
        {
            await _itemService.UpdateItemAsync(id, itemDto);
            return Ok(new {success = true, data = itemDto, message = $"Item with ID {id} successfully updated" });
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _itemService.DeleteItemAsync(id);
            return Ok(new { success = true, message = $"Item with ID {id} successfully deleted" });
        }
    }
}
