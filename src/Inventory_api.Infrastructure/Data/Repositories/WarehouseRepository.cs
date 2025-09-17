using Inventory_api.src.Core.Entities;
using Inventory_api.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory_api.Infrastructure.Data.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AppDbContext _context;
        public WarehouseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Warehouse>> GetAll()
        {
            return await _context.Warehouses
                .AsNoTracking()
                .Include(w => w.Location)
                .Include(w => w.Inventories)
                .ToListAsync();
        }

        public async Task<Warehouse?> GetById(int id)
        {
            return await _context.Warehouses
                .AsNoTracking()
                .Include(w => w.Location)
                .Include(w => w.Inventories)
                .FirstOrDefaultAsync(w => w.WarehouseId == id);
        }

        public async Task<IEnumerable<Warehouse>> GetByName(string name)
        {
            return await _context.Warehouses
                .AsNoTracking()
                .Include(w => w.Location)
                .Include(w => w.Inventories)
                .Where(w => w.WarehouseName.ToLower() == name.ToLower())
                .ToListAsync();
        }

        public async Task AddAsync(Warehouse warehouse)
        {
            await _context.Warehouses.AddAsync(warehouse);
        }

        public void Update(Warehouse warehouse)
        {
            _context.Warehouses.Update(warehouse);
        }

        public void Delete(Warehouse warehouse)
        {
            _context.Warehouses.Remove(warehouse);
        }
    }
}
