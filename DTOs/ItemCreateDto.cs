namespace API_Manajemen_Barang.DTO
{
    public class ItemCreateDto
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
