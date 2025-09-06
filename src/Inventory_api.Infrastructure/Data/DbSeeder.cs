using Inventory_api.src.Core.Entities;
using Inventory_api.src.Core.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_api.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            // Seed Roles
            if (!await context.Roles.AnyAsync())
            {
                await context.Roles.AddRangeAsync(
                    new Role { RoleName = "admin" },
                    new Role { RoleName = "staff" }
                );
                await context.SaveChangesAsync();
            }

            // Seed User
            if (!await context.Users.AnyAsync())
            {
                var adminRole = await context.Roles.FirstAsync(r => r.RoleName == "admin");

                await context.Users.AddAsync(new User
                {
                    Username = "Admin",
                    Name = "Admin",
                    Email = "admin@gmail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    RoleId = adminRole.RoleId
                });
                await context.SaveChangesAsync();
            }

            // Seed Categories
            if (!await context.Categories.AnyAsync())
            {
                await context.Categories.AddRangeAsync(
                    new Category { Name = "Elektronik" },
                    new Category { Name = "Pakaian" },
                    new Category { Name = "Makanan" },
                    new Category { Name = "Minuman" },
                    new Category { Name = "Peralatan Rumah Tangga" }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}