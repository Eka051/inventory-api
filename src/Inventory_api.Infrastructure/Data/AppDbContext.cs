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
        public DbSet<Location> Locations { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            // User -> Role (many User to one Role)
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Inventory: composite key (ItemId, WarehouseId)
            modelBuilder.Entity<Inventory>()
                .HasKey(inv => new { inv.ItemId, inv.WarehouseId });

            // Inventory -> Item (many Inventory to one Item)
            modelBuilder.Entity<Item>()
                .HasMany(i => i.Inventories)
                .WithOne(inv => inv.Item)
                .HasForeignKey(inv => inv.ItemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Inventory -> Warehouse (many Inventory to one Warehouse)
            modelBuilder.Entity<Warehouse>()
                .HasMany(w => w.Inventories)
                .WithOne(inv => inv.Warehouse)
                .HasForeignKey(inv => inv.WarehouseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Warehouse -> Location (many Warehouse to one Location)
            modelBuilder.Entity<Warehouse>()
                .HasOne(w => w.Location)
                .WithMany() 
                .HasForeignKey(w => w.LocationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Category -> Item (many Item to one Category)
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Category)
                .HasForeignKey(i => i.CategoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Item -> StockMovement (many StockMovement to one Item)
            modelBuilder.Entity<Item>()
                .HasMany(i => i.StockMovements)
                .WithOne(sm => sm.Item)
                .HasForeignKey(sm => sm.ItemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Province -> City (many City to one Province)
            modelBuilder.Entity<Province>()
                .HasMany(p => p.Cities)
                .WithOne(c => c.Province)
                .HasForeignKey(c => c.ProvinceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // City -> District (many District to one City)
            modelBuilder.Entity<City>()
                .HasMany(c => c.Districts)
                .WithOne(d => d.City)
                .HasForeignKey(d => d.CityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // PurchaseOrder -> PurchaseOrderItem (many POItem to one PO)
            modelBuilder.Entity<PurchaseOrder>()
                .HasMany(po => po.Items)
                .WithOne(poi => poi.PurchaseOrder)
                .HasForeignKey(poi => poi.PurchaseOrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // PurchaseOrderItem -> Item (many POItem to one Item)
            modelBuilder.Entity<PurchaseOrderItem>()
                .HasOne(poi => poi.Item)
                .WithMany()
                .HasForeignKey(poi => poi.ItemId)
                .IsRequired();
        }
    }
}
