using API_Manajemen_Barang.Data;
using API_Manajemen_Barang.DTOs;
using API_Manajemen_Barang.Helpers;
using API_Manajemen_Barang.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllStaff()
        {
            try
            {
                var staffList = _context.Users.Include(u => u.Role).Where(u => u.Role.RoleName.ToLower() == "staff").Select(u => new UserResponseDto
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    RoleName = u.Role.RoleName.ToLower(),
                }).ToList();
                if (staffList == null || !staffList.Any())
                {
                    return NotFound(new { success = false, message = "Data staff tidak ditemukan" });
                }
                return Ok(new { success = true, data = staffList });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost]
        [Route("staff")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateStaff([FromBody] UserCreateDto userDto)
        {
            try
            {
                var role = _context.Roles.FirstOrDefault(r => r.RoleName.ToLower() == "staff");
                if (role == null)
                {
                    return NotFound(new { success = false, message = "Role staff tidak ditemukan. Silahkan tambah terlebih dahulu" });
                }

                var existingUser = _context.Users.FirstOrDefault(u => u.Email.ToLower() == userDto.Email.ToLower());
                if (existingUser != null)
                {
                    return Conflict(new { success = false, message = "Email sudah terdaftar" });
                }

                var hashedPassword = PasswordHasherHelper.Hash(userDto.Password);

                var user = new User
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    PasswordHash = hashedPassword,
                    RoleId = role.RoleId

                };
                _context.Users.Add(user);
                _context.SaveChanges();

                var response = new UserResponseDto
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    RoleName = role.RoleName.ToLower(),
                };
                return CreatedAtAction(nameof(GetAllStaff),new { success = true, data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPut]
        [Route("staff/{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateStaff(int id, [FromBody] UserCreateDto userDto)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == id);
                if (user == null)
                {
                    return NotFound(new { success = false, message = "Staff tidak ditemukan" });
                }
                var existingUser = _context.Users.FirstOrDefault(u => u.Email.ToLower() == userDto.Email.ToLower() && u.UserId != id);
                if (existingUser != null)
                {
                    return Conflict(new { success = false, message = "Email sudah terdaftar" });
                }
                user.Name = userDto.Name;
                user.Email = userDto.Email;
                if (!string.IsNullOrEmpty(userDto.Password))
                {
                    user.PasswordHash = PasswordHasherHelper.Hash(userDto.Password);
                }
                _context.Users.Update(user);
                _context.SaveChanges();
                var response = new UserResponseDto
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    RoleName = user.Role.RoleName.ToLower(),
                };
                return Ok(new { success = true, data = response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpDelete]
        [Route("staff/{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
                return Ok(new { success = true, message = $"Staff dengan userId {id} berhasil dihapus" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
