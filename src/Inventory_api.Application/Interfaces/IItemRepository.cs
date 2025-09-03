using Inventory_api.src.Core.Entities;

namespace Inventory_api.src.Application.Interfaces
{
    public interface IItemRepository
    {
        Task<Item?> GetByIdAsync(int itemId);
        Task<IEnumerable<Item>> GetAllAsync();
        Task<IEnumerable<Item>> FindByNameAsync(string name);
        Task<bool> IsItemNameExistAsync(string name);
        Task AddAsync(Item item);
        void Update(Item item);
        void Delete(Item item);
    }
}
