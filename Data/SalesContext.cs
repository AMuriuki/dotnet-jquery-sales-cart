using Microsoft.EntityFrameworkCore;

namespace sales_invoicing_dotnet.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {

        }

        public DbSet<Product>? Products { get; set; }
        public DbSet<Invoice>? Invoices { get; set; }
        public DbSet<SoldProduct>? SoldProducts { get; set; }
        public DbSet<Customer>? Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoldProduct>().HasKey(sp => new { sp.InvoiceId, sp.ProductId });

            modelBuilder.Entity<SoldProduct>().HasOne(sp => sp.Invoice).WithMany(s => s.SoldProducts).HasForeignKey(sp => sp.InvoiceId);

            modelBuilder.Entity<SoldProduct>().HasOne(sp => sp.Product).WithMany(p => p.SoldProducts).HasForeignKey(sp => sp.ProductId);

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Apple AirPods Pro", Price = 8500.00M, imageUrl = "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/31/2411421/1.jpg?5248" },
                new Product { Id = 2, Name = "Xiaomi Redmi Buds", Price = 2700.00M, imageUrl = "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/05/7399421/1.jpg?3641" },
                new Product { Id = 3, Name = "Oraimo Wireless Earphone", Price = 3400.00M, imageUrl = "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/94/1328411/1.jpg?9977" },
                new Product { Id = 4, Name = "Netac USB Type-C 128 GB", Price = 1600.00M, imageUrl = "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/71/283233/1.jpg?8328" },
                new Product { Id = 5, Name = "Sandisk USB 32GB ", Price = 8500.00M, imageUrl = "https://ke.jumia.is/unsafe/fit-in/500x500/filters:fill(white)/product/01/191691/1.jpg?4986" }
                );

            modelBuilder.Entity<Invoice>().HasOne<Customer>(i => i.Customer).WithMany(c => c.Invoices).HasForeignKey(i => i.CustomerId);

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, fname = "Wanjiku", lname = "Mwangi" },
                new Customer { Id = 2, fname = "Kipchoge", lname = "Keino" },
                new Customer { Id = 3, fname = "Auma", lname = "Obama" },
                new Customer { Id = 4, fname = "Mumbi", lname = "Ngugi" },
                new Customer { Id = 5, fname = "Omondi", lname = "Odinga" }
            );
        }

    }
}