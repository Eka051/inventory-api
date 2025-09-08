using Inventory_api.Application.Interfaces;
using Inventory_api.src.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Inventory_api.WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("staff")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStaff()
        {
            var user = await _userService.GetAllUserAsync();
            return Ok(new {success = true, data = user});
        }

        [HttpPost]
        [Route("staff")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateStaff([FromBody] UserCreateDto userDto)
        {
            var createdUser = await _userService.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetAllStaff),new { success = true, data = createdUser});
        }

        [HttpPut]
        [Route("staff/{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStaff(int userId, [FromBody] UserCreateDto userDto)
        {
            await _userService.UpdateUserAsync(userId, userDto);
            return Ok(new {success = true, message = $"User with ID {userId} updated successfully", data = userDto});
        }

        [HttpDelete]
        [Route("staff/{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok(new {success = true, message = $"User with ID {id} deleted successfully" });
        }
    }
}
