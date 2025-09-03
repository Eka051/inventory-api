using System.ComponentModel.DataAnnotations;

namespace Inventory_api.src.Application.DTOs
{
    public class CategoryCreateDto
    {
        public string? Name { get; set; }
    }

    public class CategoryResponseDto
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
    }
}
