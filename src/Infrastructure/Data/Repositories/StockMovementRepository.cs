using API_Manajemen_Barang.src.Application.Interfaces;
using Inventory_api.src.Core.Entities;
using Inventory_api.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API_Manajemen_Barang.src.Infrastructure.Data.Repositories
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
            return await _context.StockMovements.OrderByDescending(sm => sm.CreatedAt).ToListAsync();
        }

        public async Task AddAsync(StockMovement stockMovement)
        {
            await _context.AddAsync(stockMovement);
        }

    }
}
