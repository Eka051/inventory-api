using Inventory_api.src.Application.DTOs;
using Inventory_api.src.Application.Interfaces;
using Inventory_api.src.Infrastructure.Data;
using Inventory_api.src.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Inventory_api.src.API.Controllers
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
        [ProducesResponseType(typeof(LoginDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            return Ok(new { Token = token });
        }
    }
}
