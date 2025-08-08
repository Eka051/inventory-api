namespace API_Manajemen_Barang.src.Application.DTOs
{
    public class ItemResponseDto
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
