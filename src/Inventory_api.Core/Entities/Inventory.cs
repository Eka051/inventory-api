namespace Inventory_api.src.Core.Entities
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public Ulid ItemId { get; set; }
        public Item Item { get; set; } = null!;
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;
        public int Quantity { get; set; }
        public int ReserveQuantity { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
