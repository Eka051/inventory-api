using Inventory_api.src.Application.Interfaces;
using Inventory_api.src.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory_api.Infrastructure.Data.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        public readonly AppDbContext _context;
        public InventoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Inventory?> GetByItemIdAndWarehouseIdAsync(int itemId, int warehouseId)
        {
            return await _context.Inventories.FirstOrDefaultAsync(inv => inv.ItemId == itemId && inv.WarehouseId == warehouseId);
        }

        public async Task AddAsync(Inventory inventory)
        {
            await _context.Inventories.AddAsync(inventory);
        }

        public void UpdateAsync(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
        }
    }
}
