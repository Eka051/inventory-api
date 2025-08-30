namespace Inventory_api.src.Core.Entities
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = null!;
        public bool IsActive { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Inventory> Inventories { get; set; } = null!;
    }
}
