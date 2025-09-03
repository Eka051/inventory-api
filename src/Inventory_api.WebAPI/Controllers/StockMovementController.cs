using Inventory_api.src.Application.Services;
using Inventory_api.src.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Inventory_api.src.Application.Interfaces;

namespace Inventory_api.src.API.Controllers
{
    [Route("api/stock-movements")]
    [ApiController]
    public class StockMovementController : ControllerBase
    {
        private readonly IStockMovementService _stockMovementService;
        public StockMovementController(IStockMovementService stockMovementService)
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

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(StockMovementResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStockMovementById(int stockMovementId)
        {
            var stockMovements = await _stockMovementService.GetByIdAsync(stockMovementId);
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
            var createdEntity = await _stockMovementService.CreateaStockMovementAsync(stockMovementDto);

            var responseDto = new StockMovementResponseDto
            {
                StockMovementId = createdEntity.StockMovementId,
                ItemId = createdEntity.ItemId,
                Quantity = createdEntity.Quantity,
                MovementType = stockMovementDto.MovementType,
                Note = createdEntity.Note,
                CreatedAt = createdEntity.CreatedAt
            };

            return CreatedAtAction(nameof(GetAllStockMovements), new {id = responseDto.StockMovementId}, new {success = true, data = responseDto});
        }
    }
}
