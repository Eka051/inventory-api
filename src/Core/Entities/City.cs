namespace Inventory_api.src.Core.Entities
{
    public class City
    {
        public int CityId { get; set; }
        public required string CityName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<District>? Districts { get; set; }
        public int ProvinceId { get; set; }
        public virtual required Province Province { get; set; }
    }
}
