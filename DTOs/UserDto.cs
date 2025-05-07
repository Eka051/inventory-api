namespace API_Manajemen_Barang.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public UserDto(string name, string email, string passwordHash, int roleId, string roleName)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            RoleId = roleId;
            RoleName = roleName;
        }
    }
}
