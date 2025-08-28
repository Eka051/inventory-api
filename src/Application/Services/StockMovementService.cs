using API_Manajemen_Barang.src.Application.Exceptions;
using API_Manajemen_Barang.src.Application.Interfaces;
using API_Manajemen_Barang.src.Core.Entities;
using API_Manajemen_Barang.src.Infrastructure.Data.Repositories;
using Inventory_api.src.Application.DTOs;
using Inventory_api.src.Core.Entities;
using Inventory_api.src.Infrastructure.Data.Repositories;

namespace API_Manajemen_Barang.src.Application.Services
{
    public class StockMovementService : IStockMovementService
    {
        private readonly StockMovementRepository _stockMovementRepository;
        private readonly ItemRepository _itemRepository;
        private readonly Warehouse _warehouse;

        public StockMovementService(StockMovementRepository stokRepository, ItemRepository itemRepository, Warehouse warehouse)
        {
            _stockMovementRepository = stokRepository;
            _itemRepository = itemRepository;
            _warehouse = warehouse;
        }
        public async Task<IEnumerable<StockMovementResponseDto>> GetAllAsync()
        {
            var stockMovement = await _stockMovementRepository.GetAllStock();
            if (stockMovement == null)
            {
                throw new NotFoundException("Stock Not Found");
            }
            var stockMovementDto = stockMovement.Select(st => new StockMovementResponseDto 
            { 
                StockMovementId = st.StockMovementId,
                ItemId = st.ItemId,
                Quantity = st.Quantity,
            });
            return stockMovementDto;
        }

        public async Task CreateaStockMovementAsync(StockMovementCreateDto createDto)
        {
            var stockMovement = new StockMovementCreateDto
            {
                ItemId = createDto.ItemId,
                MovementType = createDto.MovementType,
                Quantity = createDto.Quantity,
            };

            var item = new Item
            {
                Stock = createDto.Quantity,
            };

            await _itemRepository.AddAsync(item);
            //_stockMovementRepository.AddStock(stockMovement);

        }
    }
}
