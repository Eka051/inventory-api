using Inventory_api.src.Application.DTOs;

namespace API_Manajemen_Barang.src.Application.Interfaces
{
    public interface IStockMovementService
    {
        Task<IEnumerable<StockMovementResponseDto>> GetAllAsync();
        Task CreateaStockMovementAsync(StockMovementCreateDto stockMovementCreateDto);
    }
}
