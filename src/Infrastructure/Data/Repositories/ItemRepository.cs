using API_Manajemen_Barang.src.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Manajemen_Barang.src.Infrastructure.Data.Repositories
{
    public class ItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> GetAllItem() 
        {
            return await _context.Items.Include((i) => i.Category).ToListAsync();
        }

        public async Task<List<Item>> GetItemByName(string name)
        {
            return await _context.Items.Include((i) => i.Category).Where((i) => i.Name.Contains(name.ToLower())).ToListAsync();
        }
    }
}
