namespace API_Manajemen_Barang.DTO
{
    public class ItemCreateDto
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public ItemCreateDto(string name, int stock, string unit, string description, int categoryId)
        {
            Name = name;
            Stock = stock;
            Unit = unit;
            Description = description;
            CategoryId = categoryId;
        }

    }
}
