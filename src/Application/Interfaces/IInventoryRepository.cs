using API_Manajemen_Barang.src.Core.Entities;

namespace API_Manajemen_Barang.src.Application.Interfaces
{
    public interface IInventoryRepository
    {
        Task<Inventory?> GetByItemIdAndWarehouseIdAsync(int itemId, int warehouseId);
        Task AddAsync(Inventory inventory);
        void UpdateAsync(Inventory inventory);
    }
}
