using API_Manajemen_Barang.src.Core.Entities;

namespace API_Manajemen_Barang.src.Application.Interfaces
{
    public interface IItemRepository
    {
        Task<Item> GetByIdAsync(int itemId);
        Task<IEnumerable<Item>> GetAllAsync();
        Task<IEnumerable<Item>> FindByNameAsync(string name);
        Task<bool> IsItemNameExistAsync(string name);
        Task AddAsync(Item item);
        void Update(Item item);
        void Delete(Item item);
    }
}
