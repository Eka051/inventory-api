namespace Inventory_api.src.Core.Entities
{
    public class Province
    {
        public int ProvinceId { get; set; }
        public required string ProvinceName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<City>? Cities { get; set; }

    }
}
