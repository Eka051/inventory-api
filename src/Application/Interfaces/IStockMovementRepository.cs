using Inventory_api.src.Core.Entities;

namespace API_Manajemen_Barang.src.Application.Interfaces
{
    public interface IStockMovementRepository
    {
        Task<IEnumerable<StockMovement>> GetAllAsync();
        Task AddAsync(StockMovement stockMovement);
    }
}
