using API_Manajemen_Barang.src.Core.Entities;

namespace API_Manajemen_Barang.src.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
