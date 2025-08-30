using Inventory_api.src.Core.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace Inventory_api.src.Application.DTOs
{
    public class StockMovementCreateDto
    {
        public int ItemId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }
        public StockMovementType MovementType { get; set; } 
        public string? Note { get; set; }
    }

    public class StockMovementResponseDto
    {
        public int StockMovementId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public StockMovementType MovementType { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
