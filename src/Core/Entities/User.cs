using System.ComponentModel.DataAnnotations;

namespace Inventory_api.src.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; } 
        public Role Role { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }
}
