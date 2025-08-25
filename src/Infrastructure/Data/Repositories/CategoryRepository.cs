using API_Manajemen_Barang.src.Application.Interfaces;
using Inventory_api.src.Core.Entities;
using Inventory_api.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API_Manajemen_Barang.src.Infrastructure.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories.Include(i => i.Items).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.Include(i => i.Items).FirstOrDefaultAsync(c => c.CategoryId == id);
        }
    }
}
