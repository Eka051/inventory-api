using Inventory_api.src.Infrastructure.Data;

namespace API_Manajemen_Barang.src.Infrastructure.Data
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
