using Inventory_api.src.Core.Entities;

namespace Inventory_api.src.Application.Interfaces
{
    public interface IInventoryRepository
    {
        Task<Inventory?> GetByItemIdAndWarehouseIdAsync(Ulid itemId, int warehouseId);
        Task AddAsync(Inventory inventory);
        void UpdateAsync(Inventory inventory);
    }
}
