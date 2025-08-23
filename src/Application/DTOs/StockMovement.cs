using System.ComponentModel.DataAnnotations;

namespace Inventory_api.src.Application.DTOs
{
    public class StockMovementCreateDto
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string? MovementType { get; set; } // "in" or "out"
        public string? Note { get; set; }
    }

    public class StockMovementResponseDto
    {
        public int StockMovementId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public string? MovementType { get; set; } // "in" or "out"
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
