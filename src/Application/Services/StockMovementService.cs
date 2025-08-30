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
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StockMovementService(IStockMovementRepository stockMovement, IInventoryRepository inventory, IUnitOfWork unitOfWork)
        {
            _stockMovementRepository = stockMovement;
            _inventoryRepository = inventory;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateaStockMovementAsync(StockMovementCreateDto createDto)
        {
            var inventory = await _inventoryRepository.GetByItemIdAndWarehouseIdAsync(createDto.ItemId, createDto.WarehouseId);

            if (inventory == null)
            {
                throw new NotFoundException($"Tidak ada catatan inventaris untuk ItemId {createDto.ItemId} di WarehouseId {createDto.WarehouseId}.");
            }

            switch (createDto.MovementType.ToString().ToLower())
            {
                case "in":
                    inventory.Quantity += createDto.Quantity;
                    break;
                case "out":
                    if (inventory.Quantity <  0)
                    {
                        throw new BadRequestException("Stok item tidak mencukupi");
                    }
                    inventory.Quantity -= createDto.Quantity;
                    break;
                default:
                    throw new BadRequestException("Tipe pergerakan invalid. Gunakan 'in' atau 'out'.");
            }

            var stockMovement = new StockMovement
            {
                ItemId = createDto.ItemId,
                WarehouseId = createDto.WarehouseId,
                Quantity = createDto.Quantity,
                Type = createDto.MovementType,
                Note = createDto.Note,
                CreatedAt = DateTime.UtcNow,
            };

            await _stockMovementRepository.AddAsync(stockMovement);
            await _unitOfWork.SaveChangesAsync();

        }
        public async Task<IEnumerable<StockMovementResponseDto>> GetAllAsync()
        {
            var stockMovement = await _stockMovementRepository.GetAllAsync();
            
            if (!stockMovement.Any())
            {
                return new List<StockMovementResponseDto>();
            }

            var response = stockMovement.Select(sm => new StockMovementResponseDto
            {
                StockMovementId = sm.StockMovementId,
                ItemId = sm.ItemId,
                Quantity = sm.Quantity,
                MovementType = sm.Type,
                CreatedAt = sm.CreatedAt,
            });

            return response;
        }
    }
}
