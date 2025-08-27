using Inventory_api.src.Core.Entities;

namespace API_Manajemen_Barang.src.Application.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllCategoryAsync();
        public Task<Category?> GetByIdAsync(int id);
        public Task<Category?> GetByNameAsync(string name);
        public Task<bool> IsCategoryExist(string name);
        Task AddAsync(Category category);
        void UpdateAsync(Category category);
        void DeleteAsync(Category category);
    }
}
