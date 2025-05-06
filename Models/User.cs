namespace API_Manajemen_Barang.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public int RoleId { get; set; } 
        public Role Role { get; set; } // "admin" or "staff"
    }
}
