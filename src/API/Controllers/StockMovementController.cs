using Inventory_api.src.Application.DTOs;
using Inventory_api.src.Core.Entities;
using Inventory_api.src.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_api.src.API.Controllers
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
        [ProducesResponseType(typeof(StockMovementResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllStockMovements()
        {
            var stockMovements = _context.StockMovements.ToList();
            if (stockMovements == null || !stockMovements.Any())
            {
                return NotFound(new { success = false, message = "Data pergerakan stok tidak ditemukan" });
            }
            var response = stockMovements.Select(sm => new StockMovementResponseDto
            {
                StockMovementId = sm.StockMovementId,
                ItemId = sm.ItemId,
                MovementType = sm.Type,
                Quantity = sm.Quantity,
                Note = sm.Note,
                CreatedAt = sm.CreatedAt
            }).ToList();

            return Ok(new { success = true, message = response });
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(StockMovementResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateStockMovement([FromBody] StockMovementCreateDto stockMovementDto)
        {
            if (stockMovementDto == null)
            {
                return BadRequest(new { success = false, message = "Data pergerakan stok tidak valid" });
            }

            try
            {
                var item = _context.Items.FirstOrDefault(i => i.ItemId == stockMovementDto.ItemId);
                if (item == null)
                {
                    return NotFound(new { success = false, message = "Item tidak ditemukan" });
                }

                if (stockMovementDto.Quantity <= 0)
                {
                    return BadRequest(new { success = false, message = "Jumlah pergerakan stok tidak valid" });
                }

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                  .Select(e => e.ErrorMessage)
                                                  .ToList();
                    return BadRequest(new { success = false, message = "Data pergerakan stok tidak valid", errors });
                }

                switch (stockMovementDto.MovementType)
                {
                    case "in":
                        item.Stock += stockMovementDto.Quantity;
                        break;
                    case "out":
                        if (item.Stock < stockMovementDto.Quantity)
                        {
                            return BadRequest(new { success = false, message = "Stok tidak mencukupi" });
                        }
                        item.Stock -= stockMovementDto.Quantity;
                        break;
                    default:
                        return BadRequest(new { success = false, message = "Tipe pergerakan stok tidak valid" });
                }

                var stockMovement = new StockMovement
                {
                    ItemId = stockMovementDto.ItemId,
                    Type = stockMovementDto.MovementType,
                    Quantity = stockMovementDto.Quantity,
                    Note = stockMovementDto.Note!,
                    CreatedAt = DateTime.UtcNow
                };

                _context.StockMovements.Add(stockMovement);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetAllStockMovements), new { success = true, message = "Pembuatan pergerakan stok berhasil." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Terjadi kesalahan pada server", error = ex.Message });
            }
        }
    }
}
