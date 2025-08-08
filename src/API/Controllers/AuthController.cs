using API_Manajemen_Barang.src.Application.DTOs;
using API_Manajemen_Barang.src.Infrastructure.Data;
using API_Manajemen_Barang.src.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Manajemen_Barang.src.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
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
        [ProducesResponseType(typeof(LoginDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
