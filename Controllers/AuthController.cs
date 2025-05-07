using API_Manajemen_Barang.Data;
using API_Manajemen_Barang.DTOs;
using API_Manajemen_Barang.Helpers;
using API_Manajemen_Barang.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Manajemen_Barang.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            try
            {
                var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == dto.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                {
                    return Unauthorized(new { success = false, message = "Email atau Password salah" });
                }

                JwtHelper jwtHelper = new JwtHelper(_config);
                var token = jwtHelper.GenerateJwt(user);
                return Ok(new { Token = token });
            }
            catch (Npgsql.PostgresException ex)
            {
                return StatusCode(500, new { success = false, message = ex.ToString() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }


}
