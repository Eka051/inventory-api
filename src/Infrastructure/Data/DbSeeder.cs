using Inventory_api.src.Core.Entities;

namespace Inventory_api.src.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { RoleName = "admin" },
                    new Role { RoleName = "staff" }
                );
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var adminRole = context.Roles.First(r => r.RoleName == "admin");

                context.Users.Add(new User
                {
                    Username = "Admin",
                    Name = "Admin",
                    Email = "admin@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    RoleId = adminRole.RoleId
                });

                context.SaveChanges();
            }

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Elektronik" },
                    new Category { Name = "Pakaian" },
                    new Category { Name = "Makanan" },
                    new Category { Name = "Minuman" },
                    new Category { Name = "Peralatan Rumah Tangga" }
                );

                context.SaveChanges();
            }
        }
    }
}
