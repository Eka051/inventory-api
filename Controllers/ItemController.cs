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
    [Route("api/items")]
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("search")]
        [Authorize]
        public async Task<IActionResult> GetItemByName([FromBody] dynamic body)
        {
            string name = body.name;
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(new { success = false, message = "Nama barang tidak boleh kosong" });
            }

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
        public async Task<IActionResult> CreateItem([FromBody] ItemDto dto)
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

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] ItemDto dto)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound(new { success = false, message = "Barang tidak ditemukan" });
            }
            item.Name = dto.Name;
            item.Stock = dto.Stock;
            item.Description = dto.Description;
            item.CategoryId = dto.CategoryId;
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, data = item });
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(Item), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Item), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Item), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Item), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound(new { success = false, message = "Barang tidak ditemukan" });
            }
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Barang berhasil dihapus" });
        }
    }
}
