using Inventory_api.Application.Interfaces;
using Inventory_api.src.Application.DTOs;
using Inventory_api.src.Application.Exceptions;
using Inventory_api.src.Application.Interfaces;

namespace Inventory_api.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseDto> GetUserByUsername(string username)
        {
            if (username == null)
            {
                throw new BadRequestException(nameof(username));
            }

            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                throw new NotFoundException($"User with username {username} not found");
            }

            return new UserResponseDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                RoleName = user.Role.RoleName
            };
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUserAsync()
        {
            var user = await _userRepository.GetAllAsync();
            if (user == null)
            {
                throw new NotFoundException($"User data is empty");
            }

            var response = user.Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                Name = u.Name,
                Email = u.Email,
                RoleName = u.Role.RoleName
            });

            return response;
        }

        public async Task<UserResponseDto> CreateUserAsync(UserCreateDto userCreateDto)
        {
            if (userCreateDto == null)
            {
                throw new BadRequestException($"Field can't be empty");
            }

            var userExist = await _userRepository.isExist(userCreateDto.Name);
            if (userExist)
            {
                throw new ConflictException($"User with name {userCreateDto.Name} already exists");
            }

            return new UserResponseDto
            {
                Name = userCreateDto.Name,
                Email = userCreateDto.Email,
            };
        }

        public async Task UpdateUserAsync(int userId, UserCreateDto userCreateDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {userId} not found");
            }

            user.Name = userCreateDto.Name;
            user.Email = userCreateDto.Email;
            user.Password = userCreateDto.Password;

            _userRepository.Update(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {userId} not found");
            }

            _userRepository.Delete(user);
        }
    }
}
