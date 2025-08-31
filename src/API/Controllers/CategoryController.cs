using Inventory_api.src.Application.Services;
using Inventory_api.src.Application.DTOs;
using Inventory_api.src.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_api.src.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(new { success = true, data = categories });
        }

        [HttpGet("{categoryId:int}")]
        [Authorize]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            return Ok(new { success = true, data = category });
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryDto)
        {
            var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { categoryId = createdCategory.CategoryId }, new { success = true, data = createdCategory });
        }

        [HttpPut("{id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryCreateDto categoryDto)
        {
            await _categoryService.UpdateCategoryAsync(id, categoryDto);
            return Ok(new {success = true, data = categoryDto, message = $"Category with ID {id} successfully updated"});
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok(new { success = true, message = $"Category with ID {id} successfully deleted" });
        }
    }
}
