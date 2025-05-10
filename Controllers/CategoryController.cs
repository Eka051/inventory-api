using API_Manajemen_Barang.Data;
using API_Manajemen_Barang.DTO;
using API_Manajemen_Barang.DTOs;
using API_Manajemen_Barang.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Manajemen_Barang.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllCategories()
        {
            var categories = _context.Categories.ToList();
            var response = categories.Select(c => new CategoryResponseDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description
            }).ToList();
            if (response == null || !response.Any())
            {
                return NotFound(new { success = false, message = "Data kategori tidak ditemukan" });
            }
            return Ok(new { success = true, data = response});
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(CategoryCreateDto), StatusCodes.Status201Created)]

        public IActionResult CreateCategory([FromBody] CategoryCreateDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest(new { success = false, message = "Data kategori tidak valid" });
            }
            var existingCategory = _context.Categories.FirstOrDefault(c => c.Name.ToLower() == categoryDto.Name.ToLower());
            if (existingCategory != null)
            {
                return Conflict(new { success = false, message = "Kategori sudah ada. Tambahkan kategori lain" });
            }

            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };

            var response = new
            {
                category.Name,
                category.Description
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAllCategories), new { success = true, data = response });
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
        public IActionResult UpdateCategory([FromBody] CategoryResponseDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest(new { success = false, message = "Data kategori tidak valid" });
            }
            var existingCategory = _context.Categories.Find(categoryDto.CategoryId);
            if (existingCategory == null)
            {
                return NotFound(new { success = false, message = "Kategori tidak ditemukan" });
            }
            existingCategory.Name = categoryDto.Name;
            existingCategory.Description = categoryDto.Description;
            _context.SaveChanges();
            return Ok(new { success = true, message = "Kategori berhasil diperbarui" });
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
        public IActionResult DeleteCategory([FromBody] CategoryResponseDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest(new { success = false, message = "Data kategori tidak valid" });
            }
            var existingCategory = _context.Categories.Find(categoryDto.CategoryId);
            if (existingCategory == null)
            {
                return NotFound(new { success = false, message = "Kategori tidak ditemukan" });
            }
            _context.Categories.Remove(existingCategory);
            _context.SaveChanges();
            return Ok(new { success = true, message = "Kategori berhasil dihapus" });
        }
    }
}
