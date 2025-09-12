using Inventory_api.Application.DTOs;

namespace Inventory_api.Application.Interfaces
{
    public interface IWarehouseService
    {
        public Task<IEnumerable<WarehouseResponseDto>> GetAllWarehouseAsync();
        public Task<WarehouseResponseDto> GetWarehouseByIdAsync(int id);
        public Task<IEnumerable<WarehouseResponseDto>> GetWarehouseByNameAsync(string name);
        public Task AddWarehouseAsync(WarehouseCreateDto createDto);
        public Task UpdateWarehouseAsync(int warehouseId, WarehouseUpdateDto updateDto);
        public Task DeleteWarehouseAsync(int id);

    }
}
