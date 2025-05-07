using API_Manajemen_Barang.Data;
using API_Manajemen_Barang.DTOs;
using API_Manajemen_Barang.Helpers;
using API_Manajemen_Barang.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_Manajemen_Barang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public UserController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpGet]
        [Route("GetStaff")]
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
        [Route("CreateStaff")]
        public IActionResult CreateStaff([FromBody] UserDto dto)
        {
            try
            {
                var role = _context.Roles.FirstOrDefault(r => r.RoleName.ToLower() == "staff");
                if (role == null)
                {
                    return NotFound(new { success = false, message = "Role staff tidak ditemukan" });
                }


                var user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    PasswordHash = dto.PasswordHash,
                    RoleId = role.RoleId
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
    }
}
