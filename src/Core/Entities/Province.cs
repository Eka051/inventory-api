namespace API_Manajemen_Barang.src.Core.Entities
{
    public class Province
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public ICollection<City> cities { get; set; }

    }
}
