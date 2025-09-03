using Inventory_api.src.Application.DTOs;

namespace Inventory_api.src.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync();
        Task<CategoryResponseDto?> GetCategoryByIdAsync(int id);
        Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task UpdateCategoryAsync(int id, CategoryCreateDto categoryCreateDto);
        Task DeleteCategoryAsync(int id);
    }
}
