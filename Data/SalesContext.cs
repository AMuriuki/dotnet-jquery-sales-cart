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
        }

    }
}