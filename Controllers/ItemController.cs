using API_Manajemen_Barang.Data;
using API_Manajemen_Barang.DTO;
using API_Manajemen_Barang.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Manajemen_Barang.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{name}")]
        [Authorize]
        public async Task<IActionResult> GetItemByName(string name)
        {
            var item = await _context.Items.Include(i => i.Category).FirstOrDefaultAsync(i => i.Name == name);
            if (item == null)
            {
                return NotFound(new { success = false, message = "Barang tidak ditemukan" });
            }
            return Ok(new { success = true, data = item });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(Item),StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateItem([FromBody] ItemCreateDto dto)
        {
            var categories = await _context.Categories.AnyAsync(c => c.CategoryId == dto.CategoryId);
            if (!categories)
            {
                return NotFound(new { success = false, message = "Kategori tidak ditemukan" });
            }
            var item = new Item
            {
                Name = dto.Name,
                Stock = dto.Stock,
                Description = dto.Description,
                CategoryId = dto.CategoryId
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItemByName), new { name = item.Name }, new { success = true, data = item });
        }
    }
}
