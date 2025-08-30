using API_Manajemen_Barang.src.Application.Interfaces;
using API_Manajemen_Barang.src.Core.Entities;
using Inventory_api.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API_Manajemen_Barang.src.Infrastructure.Data.Repositories
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
