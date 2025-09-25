namespace Inventory_api.src.Core.Entities
{
    public class Item
    {
        public Ulid ItemId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int DefaultPurchasePrice { get; set; }
        public int DefaultSellingPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; } = null!;
        public virtual ICollection<StockMovement> StockMovements { get; set; } = null!;
    }
}
