using Inventory_api.src.Application.DTOs;
using Inventory_api.src.Application.Exceptions;
using Inventory_api.src.Application.Interfaces;

namespace Inventory_api.src.Application.Services
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

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByUsernameAsync(dto.Username!);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                throw new UnauthorizedException("Username or password is wrong");
            }

            var accessToken = _jwtProvider.GenerateAccessToken(user);
            var refreshToken = _jwtProvider.GenerateRefreshToken(user);

            return new LoginResponseDto
            {
                accessToken = accessToken,
                refreshToken = refreshToken,
            };
        }
    }
}
