namespace Inventory_api.src.Core.Entities
{
    public class District
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; } = null!;
    }
}
