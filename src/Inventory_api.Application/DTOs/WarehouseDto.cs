
namespace Inventory_api.Application.DTOs
{
    public class WarehouseCreateDto
    {
        public required string WarehouseName { get; set; }
        public int LocationId { get; set; }
    }

    public class WarehouseResponseDto
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set;}
        public bool IsActive { get; set; }
        public string Location {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class WarehouseUpdateDto
    {
        public required string WarehouseName { get; set; }
        public bool IsActive { get; set; }
        public int LocationId { get; set; }
    }
}
