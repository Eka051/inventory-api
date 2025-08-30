
using Inventory_api.src.Core.Entities;

namespace Inventory_api.src.Core.Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public required Category Category { get; set; }
        public int DefaultPurchasePrice { get; set; }
        public int DefaultSellingPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<Inventory>? Inventories { get; set; }
        public virtual ICollection<StockMovement>? StockMovements { get; set; }
    }
}
