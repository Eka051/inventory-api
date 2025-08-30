namespace API_Manajemen_Barang.src.Core.Entities
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public bool IsActive { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
    }
}
