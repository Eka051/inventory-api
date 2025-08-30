using Inventory_api.src.Application.Services;
using Inventory_api.src.Application.DTOs;
using Inventory_api.src.Core.Entities;
using Inventory_api.src.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inventory_api.src.API.Controllers
{
    [Route("api/stock-movements")]
    [ApiController]
    public class StockMovementController : ControllerBase
    {
        private readonly StockMovementService _stockMovementService;
        public StockMovementController(StockMovementService stockMovementService)
        {
            _stockMovementService = stockMovementService;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(StockMovementResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllStockMovements()
        {
            var stockMovements = await _stockMovementService.GetAllAsync();
            return Ok(new {success = true, data = stockMovements});
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(StockMovementResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateStockMovement([FromBody] StockMovementCreateDto stockMovementDto)
        {
            var stock = await _stockMovementService.CreateaStockMovementAsync(stockMovementDto);
            return CreatedAtAction(new {success = true, data = stock});
        }
    }
}
