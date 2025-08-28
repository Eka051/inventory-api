using API_Manajemen_Barang.src.Core.Entities;
using Inventory_api.src.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory_api.src.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inventory>()
                .HasKey(inv => new { inv.ItemId, inv.WarehouseId });

            // Item -> Inventory
            modelBuilder.Entity<Item>()
                .HasMany(i => i.Inventories)
                .WithOne(inv => inv.Item)
                .HasForeignKey(inv => inv.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // Warehouse -> Inventory
            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.Inventory)
                .WithOne(inv => inv.Warehouse)
                .HasForeignKey(inv => inv.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Warehouse ->  Location
            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.Inventory)
                .WithOne(inv => inv.Warehouse)
                .HasForeignKey(inv => inv.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Category -> Item
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Category)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Item -> StockMovement
            modelBuilder.Entity<Item>()
                .HasMany(i => i.StockMovements)
                .WithOne(sm => sm.Item)
                .HasForeignKey(sm => sm.Item)
                .OnDelete(DeleteBehavior.Cascade);

            // Province -> City
            modelBuilder.Entity<Province>()
                .HasMany(p => p.Cities)
                .WithOne(c => c.Province)
                .HasForeignKey(c => c.ProvinceId)
                .OnDelete(DeleteBehavior.Cascade);

            // City -> District
            modelBuilder.Entity<City>()
                .HasMany(c => c.Districts)
                .WithOne(d => d.City)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            // Purchase Order -> Purchase Order Item
            modelBuilder.Entity<PurchaseOrder>()
                .HasMany(po => po.Items)
                .WithOne(poi => poi.PurchaseOrder)
                .HasForeignKey(poi => poi.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseOrderItem>()
                .HasOne(poi => poi.Item)
                .WithMany()
                .HasForeignKey(poi => poi.ItemId);
        }
    }
}
