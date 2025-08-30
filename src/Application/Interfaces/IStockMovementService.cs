using Inventory_api.src.Application.DTOs;
using Inventory_api.src.Core.Entities;

namespace Inventory_api.src.Application.Interfaces
{
    public interface IStockMovementService
    {
        Task<IEnumerable<StockMovementResponseDto>> GetAllAsync();
        Task<StockMovementResponseDto> GetByIdAsync(int id);
        Task<StockMovement> CreateaStockMovementAsync(StockMovementCreateDto stockMovementCreateDto);
    }
}
