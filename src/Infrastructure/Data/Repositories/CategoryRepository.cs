using API_Manajemen_Barang.src.Application.Exceptions;
using API_Manajemen_Barang.src.Application.Interfaces;
using Inventory_api.src.Core.Entities;
using Inventory_api.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
            return await _context.Categories
                .AsNoTracking()
                .Include(i => i.Items)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .AsNoTracking()
                .Include(i => i.Items)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _context.Categories
                .AsNoTracking()
                .Include(i => i.Items)
                .Where(c => c.Name.ToLower().Contains(name.ToLower()))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsCategoryExist(string name)
        {
            return await _context.Categories
                .AsNoTracking()
                .Include(i => i.Items)
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}
