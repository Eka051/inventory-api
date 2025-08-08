using System.ComponentModel.DataAnnotations;

namespace API_Manajemen_Barang.src.Application.DTOs
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
