using API_Manajemen_Barang.src.Application.DTOs;
namespace API_Manajemen_Barang.src.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto dto);
    }
}
