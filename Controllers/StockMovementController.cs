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
        [ProducesResponseType(typeof(StockMovementCreateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllStockMovements()
        {
            var stockMovements = _context.StockMovements.ToList();
            if (stockMovements == null || !stockMovements.Any())
            {
                return NotFound(new { success = false, message = "Data pergerakan stok tidak ditemukan" });
            }
            return Ok(new { success = true, message = stockMovements });
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(StockMovementCreateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateStockMovement([FromBody] StockMovementCreateDto stockMovementDto)
        {
            if (stockMovementDto == null)
            {
                return BadRequest(new { success = false, message = "Data pergerakan stok tidak valid" });
            }

            var item = _context.Items.FirstOrDefault(i => i.ItemId == stockMovementDto.ItemId);
            if (item == null)
            {
                return NotFound(new { success = false, message = "Item tidak ditemukan" });
            }

            if (stockMovementDto.MovementType == "in")
            {
                item.Stock += stockMovementDto.Quantity;
            }
            else if (stockMovementDto.MovementType == "out")
            {
                if (item.Stock < stockMovementDto.Quantity)
                {
                    return BadRequest(new { success = false, message = "Stok tidak mencukupi" });
                }
                item.Stock -= stockMovementDto.Quantity;
            }
            else
            {
                return BadRequest(new { success = false, message = "Tipe pergerakan stok tidak valid" });
            }

            var stockMovement = new StockMovement
            {
                ItemId = stockMovementDto.ItemId,
                Type = stockMovementDto.MovementType,
                Quantity = stockMovementDto.Quantity,
                Note = stockMovementDto.Note,
                CreatedAt = DateTime.UtcNow
            };

            var response = new StockMovementResponseDto
            {
                ItemId = stockMovement.ItemId,
                MovementType = stockMovement.Type,
                Quantity = stockMovement.Quantity,
                Note = stockMovement.Note,
                CreatedAt = stockMovement.CreatedAt
            };

            _context.StockMovements.Add(stockMovement);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAllStockMovements), new { success = true, message = "Pembuatan pergerakan stok berhasil." });
        }
    }
}
