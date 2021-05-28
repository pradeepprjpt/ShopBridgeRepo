using Microsoft.EntityFrameworkCore;
using ShopBridgeApi.Models;
using System;

namespace ShopBridgeApi.DAO
{
    public class ShopBridgeContext : DbContext
    {
        public ShopBridgeContext(DbContextOptions<ShopBridgeContext> options) : base (options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>()
                .HasData(
                new Inventory
                {
                    Id = 1,
                    Name = "N95",
                    Description = "N95 Mask",
                    Price = 100,
                    Quantity = 500,
                    CreatedBy = "SYSTEM",
                    ModifiedBy = null,
                    CreatedOn = DateTime.Now.Date,
                    ModifiedOn = null
                },
                new Inventory
                {
                    Id = 2,
                    Name = "PPE Kit",
                    Description = "PPE Kit",
                    Price = 5000,
                    Quantity = 3500,
                    CreatedBy = "SYSTEM",
                    ModifiedBy = null,
                    CreatedOn = DateTime.Now.Date,
                    ModifiedOn = null
                },
                new Inventory
                {
                    Id = 3,
                    Name = "Himalaya Sanitizer",
                    Description = "Himalaya Sanitizer",
                    Price = 150,
                    Quantity = 5000,
                    CreatedBy = "SYSTEM",
                    ModifiedBy = null,
                    CreatedOn = DateTime.Now.Date,
                    ModifiedOn = null
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
