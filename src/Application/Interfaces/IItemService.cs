using API_Manajemen_Barang.src.Core.Entities;

namespace API_Manajemen_Barang.src.Application.Interfaces
{
    public interface IItemService
    {
        Task<Item?> GetAllItem();
        Task<Item?> GetItemById(int id);
        Task<Item?> GetItemByName(string name);
    }
}
