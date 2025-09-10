using Inventory_api.Application.DTOs;
using Inventory_api.Application.Interfaces;
using Inventory_api.src.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_api.Application.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public WarehouseService(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IEnumerable<WarehouseResponseDto>> GetAllWarehouseAsync()
        {
            var warehouses = await _warehouseRepository.GetAll();
            var response = warehouses.Select(w => new WarehouseResponseDto
            {
                WarehouseId = w.WarehouseId,
                WarehouseName = w.WarehouseName,
                IsActive = w.IsActive,
                //Location = w.Location.,
                CreatedAt = w.CreatedAt,
                UpdatedAt = w.UpdatedAt,
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
                IsActive = w.IsActive,
                CreatedAt= w.CreatedAt,
                UpdatedAt= w.UpdatedAt,
            });

            return warehouseDtos;
        }
    }
}
