namespace Inventory_api.src.Core.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<Item> Items { get; set; } = null!;
    }
}
