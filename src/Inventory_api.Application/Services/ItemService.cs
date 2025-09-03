using Inventory_api.src.Application.Exceptions;
using Inventory_api.src.Core.Entities;
using Inventory_api.src.Infrastructure.Data.Repositories;
using Inventory_api.src.Application.DTOs;
using Inventory_api.src.Application.Interfaces;

namespace Inventory_api.src.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;
        
        public ItemService(IItemRepository itemRepository, ICategoryRepository categoryRepository)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
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
                Stock = item.Inventories?.Sum((inventory) => inventory.Quantity) ?? 0,
                Description = item.Description ?? string.Empty,
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
                Stock = item.Inventories?.Sum((inventory) => inventory.Quantity) ?? 0,
                Description = item.Description ?? string.Empty,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.Name
            });

            return itemDtos;
        }

        public async Task<IEnumerable<ItemResponseDto>> SearchItemsByNameAsync(string name)
        {
            var item = await _itemRepository.FindByNameAsync(name);
            var itemData = item.Select(item => new ItemResponseDto
            {

                ItemId = item.ItemId,
                Name = item.Name,
                Stock = item.Inventories?.Sum((inventory) => inventory.Quantity) ?? 0,
                Description = item.Description ?? string.Empty,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.Name
            });
            return itemData;
        }

        public async Task<ItemResponseDto> CreateNewItemAsync(ItemCreateDto itemDto)
        {
            if (itemDto == null)
            {
                throw new BadRequestException("Item name can't be empty");
            }

            if (await _itemRepository.IsItemNameExistAsync(itemDto.Name))
            {
                throw new ConflictException($"Item with name {itemDto.Name} already exists");
            }

            

            var item = new Item
            {
                Name = itemDto.Name,
                Description = itemDto.Description,
                CategoryId = itemDto.CategoryId,
            };

            

            var initialInventory = new Inventory
            {
                WarehouseId = itemDto.WarehouseId,
                Quantity = itemDto.Stock,
                ReserveQuantity = 0
            };

            item.Inventories.Add(initialInventory);


            await _itemRepository.AddAsync(item);
            var category = await _categoryRepository.GetByIdAsync(item.CategoryId);
            var categoryName = category?.Name ?? string.Empty;



            return new ItemResponseDto
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Stock = item.Inventories.Sum((inventory) => inventory.Quantity),
                Description = item.Description,
                CategoryId = item.CategoryId,
                CategoryName = categoryName,
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
