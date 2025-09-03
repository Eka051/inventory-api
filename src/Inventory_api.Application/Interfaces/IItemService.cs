using Inventory_api.src.Application.DTOs;
using Inventory_api.src.Core.Entities;

namespace Inventory_api.src.Application.Interfaces
{
    public interface IItemService
    {
        Task<ItemResponseDto> GetItemByIdAsync(int itemId);
        Task<IEnumerable<ItemResponseDto>> GetAllItemsAsync();
        Task<IEnumerable<ItemResponseDto>> SearchItemsByNameAsync(string name);
        Task<ItemResponseDto> CreateNewItemAsync(ItemCreateDto itemDto);
        Task UpdateItemAsync(int itemId, ItemCreateDto itemDto);
        Task DeleteItemAsync(int itemId);
    }
}
