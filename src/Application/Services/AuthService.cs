using API_Manajemen_Barang.src.Application.DTOs;
using API_Manajemen_Barang.src.Application.Interfaces;

namespace API_Manajemen_Barang.src.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public readonly IJwtProvider _jwtProvider;

        public AuthService(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(dto.Username!);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                throw new Exception("Username or password is wrong");
            }

            var token = _jwtProvider.GenerateAccessToken(user);
            return token;
        }
    }
}
