using Inventory_api.Application.DTOs;
using Inventory_api.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_api.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WarehouseResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllWarehouse()
        {
            var warehouses = await _warehouseService.GetAllWarehouseAsync();
            return Ok(new {success = true, data = warehouses});
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<WarehouseResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWarehouseByName([FromQuery] string name)
        {
            var warehouse = await _warehouseService.GetWarehouseByNameAsync(name);
            return Ok(new { success = true, data = warehouse });
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(WarehouseResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWarehouseById(int id)
        {
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);
            return Ok(new { success = true, data = warehouse });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateWarehouse([FromBody] WarehouseCreateDto warehouseCreateDto)
        {
            try
            {
                await _warehouseService.AddWarehouseAsync(warehouseCreateDto);
                return CreatedAtAction(nameof(GetAllWarehouse), new { success = true, message = "Warehouse created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateWarehouse(int id, [FromBody] WarehouseUpdateDto warehouseUpdateDto)
        {
            try
            {
                await _warehouseService.UpdateWarehouseAsync(id, warehouseUpdateDto);
                return Ok(new { success = true, message = $"Warehouse with ID {id} updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            try
            {
                await _warehouseService.DeleteWarehouseAsync(id);
                return Ok(new { success = true, message = $"Warehouse with ID {id} deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
