using Inventory_api.src.Application.Exceptions;
using Inventory_api.src.Application.Interfaces;
using Inventory_api.src.Application.DTOs;
using Inventory_api.src.Core.Entities;

namespace Inventory_api.src.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            if (categoryCreateDto.Name == null)
            {
                throw new BadRequestException($"Category name must be filled in!");
            }

            var existingCategory = await _categoryRepository.GetByNameAsync(categoryCreateDto.Name);
            if (existingCategory != null)
            {
                throw new ConflictException($"Category with name {categoryCreateDto.Name} already exists");
            }

            var category = new Category
            {
                Name = categoryCreateDto.Name,
            };

            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return new CategoryResponseDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
            };
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoryAsync();

            return categories.Select(c => new CategoryResponseDto
            {
                CategoryId= c.CategoryId,
                Name = c.Name,
            });
        }

        public async Task<CategoryResponseDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new NotFoundException($"Category with ID {id} not found");
            }

            return new CategoryResponseDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
            };
        }

        public async Task UpdateCategoryAsync(int id, CategoryCreateDto categoryDto)
        {
            var categoryToUpdate = await _categoryRepository.GetByIdAsync(id);
            if (categoryToUpdate == null)
            {
                throw new NotFoundException($"Category with ID {id} not found");
            }

            if (categoryToUpdate.Name.ToLower() != categoryDto.Name?.ToLower())
            {
                var existingCategory = await _categoryRepository.GetByNameAsync(categoryDto.Name!.ToLower());
                if (existingCategory != null && existingCategory.CategoryId != id)
                {
                    throw new ConflictException($"Category with name {categoryDto.Name} already exists");
                }
            }

            categoryToUpdate.Name = categoryDto.Name;

            _categoryRepository.Update(categoryToUpdate);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null )
            {
                throw new NotFoundException($"Category with ID {id} not found");
            }

            _categoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
