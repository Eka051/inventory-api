namespace Inventory_api.src.Core.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }

        public Province province { get; set; } = null!;
        public City city { get; set; } = null!;
        public District district { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Warehouse> Warehouse { get; set; } = null!;
    }
}
