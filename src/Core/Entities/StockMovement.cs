using Inventory_api.src.Core.Entities.Enum;

namespace Inventory_api.src.Core.Entities
{
    public class StockMovement
    {
        public int StockMovementId { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; } = null!;
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; } = null!;
        public StockMovementType Type { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
