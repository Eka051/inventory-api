using API_Manajemen_Barang.src.Core.Entities;

namespace API_Manajemen_Barang.src.Infrastructure.Data
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
                    new Category { Name = "Elektronik", Description = "Barang elektronik" },
                    new Category { Name = "Pakaian", Description = "Barang pakaian pria & wanita" },
                    new Category { Name = "Makanan", Description = "Berbagai jenis makanan" },
                    new Category { Name = "Minuman", Description = "Berbagai jenis minuman" },
                    new Category { Name = "Peralatan Rumah Tangga", Description = "Barang-barang untuk rumah" }
                );

                context.SaveChanges();
            }
        }
    }
}
