using Inventory_api.Application.DTOs;
using Inventory_api.Application.Interfaces;
using Inventory_api.src.Application.Exceptions;
using Inventory_api.src.Application.Interfaces;
using Inventory_api.src.Core.Entities;

namespace Inventory_api.Application.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IUnitOfWork _unitOfWork;
        public WarehouseService(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork)
        {
            _warehouseRepository = warehouseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<WarehouseResponseDto>> GetAllWarehouseAsync()
        {
            var warehouses = await _warehouseRepository.GetAll();
            var response = warehouses.Select(w => new WarehouseResponseDto
            {
                WarehouseId = w.WarehouseId,
                WarehouseName = w.WarehouseName,
                IsActive = w.IsActive,
                Location = $"{w.Location.district.DistrictName}, {w.Location.city.CityName}, {w.Location.district.DistrictName}",
            });

            return response;
        }

        public async Task<WarehouseResponseDto> GetWarehouseByIdAsync(int warehouseId)
        {
            var warehouse = await _warehouseRepository.GetById(warehouseId);
            if (warehouse == null )
            {
                throw new NotFoundException($"Warehouse with ID {warehouseId} not found!");
            }

            return new WarehouseResponseDto
            {
                WarehouseId = warehouse.WarehouseId,
                WarehouseName = warehouse.WarehouseName,
                Location = $"{warehouse.Location.district.DistrictName}, {warehouse.Location.city.CityName}, {warehouse.Location.district.DistrictName}",
                IsActive = warehouse.IsActive,
                CreatedAt= warehouse.CreatedAt,
                UpdatedAt= warehouse.UpdatedAt,
            };
        }

        public async Task<IEnumerable<WarehouseResponseDto>> GetWarehouseByNameAsync(string warehouseName)
        {
            var warehouse = await _warehouseRepository.GetByName(warehouseName);
            var warehouseDtos = warehouse.Select(w => new WarehouseResponseDto
            {
                WarehouseId= w.WarehouseId,
                WarehouseName = w.WarehouseName,
                Location = $"{w.Location.district.DistrictName}, {w.Location.city.CityName}, {w.Location.district.DistrictName}",
                IsActive = w.IsActive,
                CreatedAt= w.CreatedAt,
                UpdatedAt= w.UpdatedAt,
            });

            return warehouseDtos;
        }

        public async Task AddWarehouseAsync(WarehouseCreateDto warehouseDto)
        {
            var warehouse = new Warehouse
            {
                WarehouseName = warehouseDto.WarehouseName,
                LocationId = warehouseDto.LocationId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };

            await _warehouseRepository.AddAsync(warehouse);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateWarehouseAsync(int warehouseId, WarehouseUpdateDto warehouseDto)
        {
            var warehouse = await _warehouseRepository.GetById(warehouseId);
            if (warehouse == null)
            {
                throw new NotFoundException($"Warehouse with ID {warehouseId} not found");
            }

            warehouse.WarehouseName = warehouseDto.WarehouseName;
            warehouse.IsActive = warehouseDto.IsActive;
            warehouse.LocationId = warehouseDto.LocationId;
            warehouse.UpdatedAt = DateTime.UtcNow;

            _warehouseRepository.Update(warehouse);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteWarehouseAsync(int warehouseId)
        {
            var warehouse = await _warehouseRepository.GetById(warehouseId);
            if (warehouse == null)
            {
                throw new NotFoundException($"Warehouse with ID {warehouseId} not found");
            }

            _warehouseRepository.Delete(warehouse);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
