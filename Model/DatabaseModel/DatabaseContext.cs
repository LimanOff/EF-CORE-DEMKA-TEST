using EFCore.Model.DatabaseModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Model.DatabaseModel
{
    public class DatabaseContext : DbContext
    {
        private string _connectionString = "Data Source=localhost;Initial Catalog=test;Integrated Security=True;Trust Server Certificate=True";

        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendor { get; set; }

        public DatabaseContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vendor>()
                            .HasData(
                                    new Vendor() { Id = 1, Name = "PepsiCock"},
                                    new Vendor() { Id = 2, Name = "CociCola"}
                                    );

            modelBuilder.Entity<Product>()
                            .HasData(
                                    new Product() { Id = 1, Price = 100, VendorId = 1, ImageName = "PlaceHolder.png" },
                                    new Product() { Id = 2, Price = 200, VendorId = 2, ImageName = "PlaceHolder.png" }
                                    );
        }
    }
}
