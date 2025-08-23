using API_Manajemen_Barang.src.Application.Interfaces;
using API_Manajemen_Barang.src.Core.Entities;
using API_Manajemen_Barang.src.Infrastructure.Data;

namespace API_Manajemen_Barang.src.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IItemRepository _itemRepository;
        
        public ItemService(AppDbContext appDbContext, IItemRepository itemRepository)
        {
            _appDbContext = appDbContext;
            _itemRepository = itemRepository;
        }

        public async Task<List<Item?>> GetAllItem()
        {
            return await _itemRepository.GetAllItemAsync();
        }

        public async Task<Item?> GetItemById(int id)
        {

        }

        public async Task<Item?> GetItemByName(string name)
        {

        }


    }
}
