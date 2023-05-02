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

            modelBuilder.Entity<Product>().HasIndex(p => p.sku).IsUnique();

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Apple AirPods Pro", Price = 8500.00M, sku = "PROD-001", imageUrl = "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/31/2411421/1.jpg?5248" },
                new Product { Id = 2, Name = "Xiaomi Redmi Buds", Price = 2700.00M, sku = "PROD-002", imageUrl = "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/05/7399421/1.jpg?3641" },
                new Product { Id = 3, Name = "Oraimo Wireless Earphone", Price = 3400.00M, sku = "PROD-003", imageUrl = "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/94/1328411/1.jpg?9977" },
                new Product { Id = 4, Name = "Netac USB Type-C 128 GB", Price = 1600.00M, sku = "PROD-004", imageUrl = "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/71/283233/1.jpg?8328" },
                new Product { Id = 5, Name = "Sandisk USB 32GB ", Price = 8500.00M, sku = "PROD-005", imageUrl = "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/01/191691/1.jpg?4986" }
                );
        }

    }
}