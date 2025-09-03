using Inventory_api.src.Core.Entities;

namespace Inventory_api.src.Application.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllCategoryAsync();
        public Task<Category?> GetByIdAsync(int id);
        public Task<Category?> GetByNameAsync(string name);
        public Task<bool> IsCategoryExist(string name);
        Task AddAsync(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
