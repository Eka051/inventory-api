namespace Inventory_api.src.Core.Entities
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public required string WarehouseName { get; set; }
        public bool IsActive { get; set; }
        public int LocationId { get; set; }
        public required Location Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Inventory>? Inventories { get; set; }
    }
}
