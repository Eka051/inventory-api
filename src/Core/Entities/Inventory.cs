namespace Inventory_api.src.Core.Entities
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int ItemId { get; set; }
        public virtual required Item Item { get; set; }
        public int WarehouseId { get; set; }
        public virtual required Warehouse Warehouse { get; set; }
        public int Quantity { get; set; }
        public int ReserveQuantity { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
