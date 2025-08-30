using Inventory_api.src.Infrastructure.Data;

namespace Inventory_api.src.Infrastructure.Data
{
    public class UnitOfWork
    {
        public readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
