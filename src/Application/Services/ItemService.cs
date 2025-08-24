using API_Manajemen_Barang.src.Application.Exceptions;
using Inventory_api.src.Application.DTOs;
using Inventory_api.src.Application.Interfaces;
using Inventory_api.src.Core.Entities;

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

        public async Task<IEnumerable<ItemResponseDto>> GetAllItemsAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            var itemDtos = items.Select(item => new ItemResponseDto
            {

                ItemId = item.ItemId,
                Name = item.Name,
                Stock = item.Stock,
                Description = item.Description,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.Name
            });

            return itemDtos;
        }

        public async Task<IEnumerable<ItemResponseDto>> SearchNameByNameAsync(string name)
        {
            var item = await _itemRepository.FindByNameAsync(name);
            var itemData = item.Select(item => new ItemResponseDto
            {

                ItemId = item.ItemId,
                Name = item.Name,
                Stock = item.Stock,
                Description = item.Description,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.Name
            });
            return itemData;
        }

        public async Task<ItemResponseDto> CreateNewItemAsync(ItemCreateDto itemDto)
        {
            if (await _itemRepository.IsItemNameExistAsync(itemDto.Name))
            {
                throw new ConflictException($"Item with name {itemDto.Name} already exists");
            }

            var item = new Item
            {
                Name = itemDto.Name,
                Stock = itemDto.Stock,
                Description = itemDto.Description,
                CategoryId = itemDto.CategoryId,
            };

            await _itemRepository.AddAsync(item);
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

        public async Task UpdateItemAsync(int itemId, ItemCreateDto itemDto)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);
            if (item == null)
            {
                throw new NotFoundException($"Item with ID {itemId} not found");
            }

            item.Name = itemDto.Name;
            item.Stock = itemDto.Stock;
            item.Description = itemDto.Description;
            item.CategoryId = itemDto.CategoryId;

            _itemRepository.Update(item);
        }

        public async Task DeleteItemAsync(int itemId)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);
            if (item == null)
            {
                throw new NotFoundException($"Item with ID {itemId} not found");
            }
            _itemRepository.Delete(item);
        }
    }
}
