using Microsoft.EntityFrameworkCore;

namespace sales_invoicing_dotnet.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {

        }

        public DbSet<Product>? Products { get; set; }
        public DbSet<Sale>? Sales { get; set; }
        public DbSet<SaleProduct>? SaleProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaleProduct>().HasKey(sp => new { sp.SaleId, sp.ProductId });

            modelBuilder.Entity<SaleProduct>().HasOne(sp => sp.Sale).WithMany(s => s.SaleProducts).HasForeignKey(sp => sp.SaleId);

            modelBuilder.Entity<SaleProduct>().HasOne(sp => sp.Product).WithMany(p => p.SaleProducts).HasForeignKey(sp => sp.ProductId);

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Apple AirPods Pro", Price = 8500.00M, sku = "P1" },
                new Product { Id = 2, Name = "Xiamo Redmi Buds", Price = 2700.00M, sku = "P10" },
                new Product { Id = 3, Name = "Oraimo Wireless Earphone", Price = 3400.00M, sku = "P100" },
                new Product { Id = 4, Name = "Netac USB Type-C 128 GB", Price = 1600.00M, sku = "P1000" },
                new Product { Id = 5, Name = "Sandisk USB 32GB ", Price = 8500.00M, sku = "P10000" }
                );
        }

    }
}