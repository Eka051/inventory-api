using System.ComponentModel.DataAnnotations;

namespace API_Manajemen_Barang.src.Application.DTOs
{
    public class UserCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
