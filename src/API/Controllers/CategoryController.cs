using API_Manajemen_Barang.src.Application.DTOs;
using API_Manajemen_Barang.src.Core.Entities;
using API_Manajemen_Barang.src.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Manajemen_Barang.src.API.Controllers
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
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [Authorize]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CategoryCreateDto categoryDto)
        {
            try
            {
                if (categoryDto == null)
                {
                    return BadRequest(new { success = false, message = "Data kategori tidak valid" });
                }

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                  .Select(e => e.ErrorMessage)
                                                  .ToList();
                    return BadRequest(new { success = false, message = "Data kategori tidak valid", errors });
                }

                var existingCategory = _context.Categories.FirstOrDefault(c => c.Name.ToLower() == categoryDto.Name!.ToLower());
                if (existingCategory != null)
                {
                    return Conflict(new { success = false, message = $"Kategori {existingCategory.Name} sudah ada. Tambahkan kategori lain" });
                }

                var category = new Category
                {
                    Name = categoryDto.Name!,
                    Description = categoryDto.Description!
                };

                var response = new
                {
                    category.Name,
                    category.Description
                };
                _context.Categories.Add(category);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetAllCategories), new { success = true, message = "Kategori berhasil ditambahkan", data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Terjadi kesalahan pada server", error = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryCreateDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest(new { success = false, message = "Data kategori tidak valid" });
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return BadRequest(new { success = false, message = "Data kategori tidak valid", errors });
            }
            var existingCategory = _context.Categories.Find(id);
            if (existingCategory == null)
            {
                return NotFound(new { success = false, message = "Kategori tidak ditemukan" });
            }
            else if (categoryDto.Name!.ToLower() != existingCategory.Name.ToLower())
            {
                var duplicateCategory = _context.Categories.FirstOrDefault(c => c.Name.ToLower() == categoryDto.Name.ToLower());
                if (duplicateCategory != null)
                {
                    return Conflict(new { success = false, message = $"Kategori {duplicateCategory.Name} sudah ada. Tambahkan kategori lain" });
                }
            }
            existingCategory.Name = categoryDto.Name;
            existingCategory.Description = categoryDto.Description!;
            var response = new CategoryResponseDto
            {
                CategoryId = existingCategory.CategoryId,
                Name = existingCategory.Name,
                Description = existingCategory.Description
            };
            _context.SaveChanges();
            return Ok(new { success = true, message = "Kategori berhasil diperbarui", data = response });
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCategory(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { success = false, message = "ID kategori tidak valid" });
            }
            var existingCategory = _context.Categories.Find(id);
            if (existingCategory == null)
            {
                return NotFound(new { success = false, message = $"Kategori dengan id {id} tidak ditemukan" });
            }
            _context.Categories.Remove(existingCategory);
            _context.SaveChanges();
            return Ok(new { success = true, message = $"Kategori {existingCategory.Name} dengan id {id} berhasil dihapus" });
        }
    }
}
