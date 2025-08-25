using API_Manajemen_Barang.src.Core.Entities;
using API_Manajemen_Barang.src.Core.Entities.Enum;

namespace Inventory_api.src.Core.Entities
{
    public class StockMovement
    {
        public int StockMovementId { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public StockMovementType Type { get; set; }
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
