using Microsoft.EntityFrameworkCore;
using MyInventory.Api.Models;

namespace MyInventory.Api;

public class MyInventoryDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Picture> Pictures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .ToTable("Items");

        modelBuilder.Entity<Picture>()
            .ToTable("Pictures");

        //modelBuilder.Entity<Item>()
        //    .HasKey(i => i.Id)
        //    .HasName("Primarykey_Id");
    }

    public MyInventoryDbContext(DbContextOptions<MyInventoryDbContext> options)
        : base(options)
    {
            
    }
}
