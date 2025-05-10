using API_Manajemen_Barang.Data;
using API_Manajemen_Barang.DTOs;
using API_Manajemen_Barang.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Manajemen_Barang.Controllers
{
    [Route("api/stock-movements")]
    [ApiController]
    public class StockMovementController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StockMovementController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(StockMovementDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllStockMovements()
        {
            var stockMovements = _context.StockMovements.ToList();
            if (stockMovements == null || !stockMovements.Any())
            {
                return NotFound(new { success = false, message = "Data stock movement tidak ditemukan" });
            }
            return Ok(new { success = true, message = "Get all stock movements" });
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(StockMovementDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateStockMovement([FromBody] StockMovementDto stockMovementDto)
        {
            if (stockMovementDto == null)
            {
                return BadRequest(new { success = false, message = "Data stock movement tidak valid" });
            }
            var stockMovement = new StockMovement
            {
                ItemId = stockMovementDto.ItemId,
                Quantity = stockMovementDto.Quantity,
                Type = stockMovementDto.MovementType,
                CreatedAt = DateTime.UtcNow
            };
            _context.StockMovements.Add(stockMovement);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAllStockMovements), new { id = stockMovement.StockMovementId }, new { success = true, message = "Stock movement created successfully" });
        }
    }
}
