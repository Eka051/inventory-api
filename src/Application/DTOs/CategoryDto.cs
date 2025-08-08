using System.ComponentModel.DataAnnotations;

namespace API_Manajemen_Barang.src.Application.DTOs
{
    public class CategoryCreateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class CategoryResponseDto
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
