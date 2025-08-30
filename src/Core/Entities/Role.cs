namespace Inventory_api.src.Core.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public required string RoleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
