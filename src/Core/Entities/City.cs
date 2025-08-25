namespace API_Manajemen_Barang.src.Core.Entities
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}
