using Inventory_api.Infrastructure.Data;
using Inventory_api.src.Application.Interfaces;
using Inventory_api.src.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory_api.Infrastructure.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Item?> GetByIdAsync(int itemId)
        {
            return await _context.Items.Include(i => i.Category)
                .Include(i => i.Inventories)
                .FirstOrDefaultAsync(i => i.ItemId == itemId);
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items
                .AsNoTracking()
                .Include(i => i.Category)
                .Include(i => i.Inventories)
                .ToListAsync();
        }

        public async Task<IEnumerable<Item>> FindByNameAsync(string name)
        {
            return await _context.Items
                .AsNoTracking()
                .Include(i => i.Category)
                .Include(i => i.Inventories)
                .Where(i => i.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task<bool> IsItemNameExistAsync(string name)
        {
            return await _context.Items
                .AsNoTracking()
                .AnyAsync(i => i.Name.ToLower() == name.ToLower());
        }

        public async Task AddAsync(Item item)
        {
            await _context.Items.AddAsync(item);
        }

        public void Update(Item item)
        {
            _context.Items.Update(item);
        }
        public void Delete(Item item)
        {
            _context.Items.Remove(item);
        }
    }
}
