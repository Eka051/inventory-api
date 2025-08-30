namespace Inventory_api.src.Core.Entities
{
    public class District
    {
        public int DistrictId { get; set; }
        public required string DistrictName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CityId { get; set; }
        public virtual required City City { get; set; }
    }
}
