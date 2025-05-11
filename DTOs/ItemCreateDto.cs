using System.ComponentModel.DataAnnotations;

namespace API_Manajemen_Barang.DTO
{
    public class ItemCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Stock { get; set; }
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
