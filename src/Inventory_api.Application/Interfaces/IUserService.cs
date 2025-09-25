using Inventory_api.src.Application.DTOs;

namespace Inventory_api.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> GetUserByUsername(string username);
        Task<IEnumerable<UserResponseDto>> GetAllUserAsync();
        Task<UserResponseDto> CreateUserAsync(UserCreateDto userCreateDto);
        Task UpdateUserAsync(Ulid userId, UserCreateDto userCreateDto);
        Task DeleteUserAsync(Ulid userId);
    }
}
