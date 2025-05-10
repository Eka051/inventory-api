using API_Manajemen_Barang.Data;
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
        public IActionResult GetAllCategories()
        {
            var categories = _context.Categories.ToList();
            if (categories == null || !categories.Any())
            {
                return NotFound(new { success = false, message = "Data kategori tidak ditemukan" });
            }
            return Ok(new { success = true, data = categories });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(Category), StatusCodes.Status201Created)]
        public IActionResult CreateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest(new { success = false, message = "Data kategori tidak valid" });
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAllCategories), new { id = category.CategoryId }, new { success = true, data = category });
        }
    }
}
