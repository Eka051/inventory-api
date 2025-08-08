using System.ComponentModel.DataAnnotations;

namespace API_Manajemen_Barang.src.Application.DTOs
{
    public class UserCreateDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? RoleName { get; set; }
    }
}
