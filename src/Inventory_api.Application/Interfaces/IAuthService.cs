using Inventory_api.src.Application.DTOs;
namespace Inventory_api.src.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
    }
}
