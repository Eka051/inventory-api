using System.ComponentModel.DataAnnotations;

namespace API_Manajemen_Barang.src.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public int RoleId { get; set; } 
        public Role Role { get; set; }

    }
}
