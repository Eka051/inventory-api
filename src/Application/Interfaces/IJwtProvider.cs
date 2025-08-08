using API_Manajemen_Barang.src.Core.Entities;

namespace API_Manajemen_Barang.src.Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken(User user);
    }
}
