namespace Inventory_api.src.Core.Entities
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; } = null!;
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; } = null!;
        public int Quantity { get; set; }
        public int ReserveQuantity { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
