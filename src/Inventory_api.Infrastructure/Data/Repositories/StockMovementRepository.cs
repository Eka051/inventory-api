using Inventory_api.Infrastructure.Data;
using Inventory_api.src.Application.Interfaces;
using Inventory_api.src.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory_api.Infrastructure.Data.Repositories
{
    public class StockMovementRepository : IStockMovementRepository
    {
        private readonly AppDbContext _context;
        public StockMovementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StockMovement>> GetAllAsync()
        {
            return await _context.StockMovements
                .AsNoTracking()
                .Include(sm => sm.Item)
                .Include(sm => sm.Warehouse)
                .OrderByDescending(sm => sm.CreatedAt)
                .ToListAsync();
        }

        public async Task<StockMovement?> GetByIdAsync(int id)
        {
            return await _context.StockMovements.FindAsync(id);
        }

        public async Task AddAsync(StockMovement stockMovement)
        {
            await _context.AddAsync(stockMovement);
        }

    }
}
