using Inventory_api.src.Application.DTOs;

namespace Inventory_api.src.Application.Interfaces
{
    public interface IItemService
    {
        Task<ItemResponseDto> GetItemByIdAsync(Ulid itemId);
        Task<IEnumerable<ItemResponseDto>> GetAllItemsAsync();
        Task<IEnumerable<ItemResponseDto>> SearchItemsByNameAsync(string name);
        Task<ItemResponseDto> CreateNewItemAsync(ItemCreateDto itemDto);
        Task UpdateItemAsync(Ulid itemId, ItemCreateDto itemDto);
        Task DeleteItemAsync(Ulid itemId);
    }
}
