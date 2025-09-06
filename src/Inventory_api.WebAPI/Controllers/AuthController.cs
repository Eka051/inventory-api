using Inventory_api.src.Application.DTOs;
using Inventory_api.src.Application.Exceptions;
using Inventory_api.src.Application.Interfaces;
using Inventory_api.WebAPI;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_api.WebAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        ///  To authenticate user
        /// </summary>
        /// <remarks>
        /// Endpoint: <b>POST</b> <code>/api/auth/login</code>
        /// Example: <a href="/api/auth/login">/api/auth/login</a>
        /// </remarks>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                var token = await _authService.LoginAsync(dto);
                return Ok(new { success = true, data = token });
            }
            catch (UnauthorizedException)
            {
                return Unauthorized(new { success = false, message = "Unauthorized, please authenticated / login" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}
