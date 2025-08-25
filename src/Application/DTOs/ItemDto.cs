using System.ComponentModel.DataAnnotations;

namespace Inventory_api.src.Application.DTOs
{
    public class ItemDto
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
    }

    public class ItemCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Stock { get; set; }
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int WarehouseId { get; set; }
    }

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
