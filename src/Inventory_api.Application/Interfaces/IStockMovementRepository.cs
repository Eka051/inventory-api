using Inventory_api.src.Core.Entities;

namespace Inventory_api.src.Application.Interfaces
{
    public interface IStockMovementRepository
    {
        Task<IEnumerable<StockMovement>> GetAllAsync();
        Task<StockMovement?> GetByIdAsync(int id);
        Task AddAsync(StockMovement stockMovement);
    }
}
