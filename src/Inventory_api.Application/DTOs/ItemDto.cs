using System.ComponentModel.DataAnnotations;

namespace Inventory_api.src.Application.DTOs
{
    public class ItemDto
    {
        public Ulid ItemId { get; set; }
        public required string Name { get; set; }
    }

    public class ItemCreateDto
    {
        public required string Name { get; set; }
        public int Stock { get; set; }
        public required string Description { get; set; }
        public int CategoryId { get; set; }
        public int WarehouseId { get; set; }
    }

    public class ItemResponseDto
    {
        public Ulid ItemId { get; set; }
        public required string Name { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
    }
}
