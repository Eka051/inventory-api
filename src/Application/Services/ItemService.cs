using API_Manajemen_Barang.src.Application.Exceptions;
using Inventory_api.src.Application.DTOs;
using Inventory_api.src.Application.Interfaces;

namespace Inventory_api.src.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        
        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ItemResponseDto> GetItemByIdAsync(int itemId)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);
            if (item == null)
            {
                throw new NotFoundException($"Item with ID {itemId} not found");
            }
            return new ItemResponseDto
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Stock = item.Stock,
                Description = item.Description,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.Name
            };
        }
    }
}
