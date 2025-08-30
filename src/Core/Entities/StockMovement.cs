using Inventory_api.src.Core.Entities.Enum;

namespace Inventory_api.src.Core.Entities
{
    public class StockMovement
    {
        public int StockMovementId { get; set; }
        public int ItemId { get; set; }
        public required Item Item { get; set; }
        public int WarehouseId { get; set; }
        public virtual required Warehouse Warehouse { get; set; }
        public StockMovementType Type { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
