using Inventory_api.src.Application.DTOs;
using Inventory_api.src.Core.Entities;

namespace Inventory_api.src.Application.Interfaces
{
    public interface IStockMovementService
    {
        Task<IEnumerable<StockMovementResponseDto>> GetAllAsync();
        Task<StockMovement> CreateaStockMovementAsync(StockMovementCreateDto stockMovementCreateDto);
    }
}
