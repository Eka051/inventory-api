namespace Inventory_api.src.Core.Entities
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<District>? Districts { get; set; }
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; } = null!;
    }
}
