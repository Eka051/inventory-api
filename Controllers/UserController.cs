using API_Manajemen_Barang.Data;
using API_Manajemen_Barang.DTOs;
using API_Manajemen_Barang.Helpers;
using API_Manajemen_Barang.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Manajemen_Barang.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("staff")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(User), StatusCodes.Status404NotFound)]

        public IActionResult GetAllStaff()
        {
            try
            {
                var users = _context.Users.Where(u => u.Role.RoleName.ToLower() != "admin");
                if (users == null || !users.Any())
                {
                    return NotFound(new { success = false, message = "Data staff tidak ditemukan" });
                }
                return Ok(new { success = true, data = users });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost]
        [Route("staff")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(User), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(User), StatusCodes.Status500InternalServerError)]
        public IActionResult CreateStaff([FromBody] UserDto dto)
        {
            try
            {
                var role = _context.Roles.FirstOrDefault(r => r.RoleName.ToLower() == "staff");
                if (role == null)
                {
                    return NotFound(new { success = false, message = "Role staff tidak ditemukan. Silahkan tambah terlebih dahulu" });
                }

                var hashedPassword = PasswordHasherHelper.Hash(dto.PasswordHash);

                var user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    PasswordHash = hashedPassword,
                    RoleId = 2,
                    
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetAllStaff), new { id = user.UserId }, new { success = true, data = user });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpDelete]
        [Route("staff/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteStaff(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == id);
                if (user == null)
                {
                    return NotFound(new { success = false, message = "Staff tidak ditemukan" });
                }
                _context.Users.Remove(user);
                _context.SaveChanges();
                return Ok(new { success = true, message = "Staff berhasil dihapus" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
