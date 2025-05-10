using API_Manajemen_Barang.Data;
using API_Manajemen_Barang.DTO;
using API_Manajemen_Barang.DTOs;
using API_Manajemen_Barang.Middleware;
using API_Manajemen_Barang.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [ProducesResponseType(typeof(ItemResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _context.Items.Include(i => i.Category).ToListAsync();
            if (items == null || !items.Any())
            {
                return NotFound(new { success = false, message = "Data barang tidak ditemukan" });
            }
            var response = items.Select(i => new ItemResponseDto
            {
                ItemId = i.ItemId,
                Name = i.Name,
                Stock = i.Stock,
                Description = i.Description,
                CategoryId = i.CategoryId,
            }).ToList();
            return Ok(new { success = true, data = response });
        }

        [HttpGet]
        [Route("search")]
        [Authorize]
        public async Task<IActionResult> GetItemByName(string name)
        {

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
        [ProducesResponseType(typeof(ItemResponseDto),StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateItem([FromBody] ItemCreateDto itemDto)
        {
            var categories = await _context.Categories.AnyAsync(c => c.CategoryId == itemDto.CategoryId);
            if (!categories)
            {
                return NotFound(new { success = false, message = "Kategori tidak ditemukan" });
            }

            var existingItem = await _context.Items.FirstOrDefaultAsync(i => i.Name.ToLower() == itemDto.Name.ToLower());
            if (existingItem != null)
            {
                return Conflict(new { success = false, message = "Barang sudah ada. Tambahkan barang lain" });
            }
            var item = new Item
            {
                Name = itemDto.Name,
                Stock = itemDto.Stock,
                Description = itemDto.Description,
                CategoryId = itemDto.CategoryId,
            };

            var response = new ItemResponseDto
            {
                ItemId = item.ItemId,
                Name = item.Name,
                Stock = item.Stock,
                Description = item.Description,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.Name,
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItemByName), new { name = item.Name }, new { success = true, data = response });
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(ItemCreateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] ItemCreateDto itemDto)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound(new { success = false, message = "Barang tidak ditemukan" });
            }
            item.Name = itemDto.Name;
            item.Stock = itemDto.Stock;
            item.Description = itemDto.Description;
            item.CategoryId = itemDto.CategoryId;
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, data = item });
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(ItemCreateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
