using System.ComponentModel.DataAnnotations;

namespace API_Manajemen_Barang.DTOs
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
