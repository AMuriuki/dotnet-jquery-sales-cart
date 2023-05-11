using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;

namespace sales_invoicing_dotnet.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<SoldProduct> SoldProducts { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoldProduct>().HasKey(sp => new { sp.InvoiceId, sp.ProductId });

            modelBuilder.Entity<SoldProduct>().HasOne(sp => sp.Invoice).WithMany(s => s.SoldProducts).HasForeignKey(sp => sp.InvoiceId);

            modelBuilder.Entity<SoldProduct>().HasOne(sp => sp.Product).WithMany(p => p.SoldProducts).HasForeignKey(sp => sp.ProductId);

            modelBuilder.Entity<Invoice>().HasOne<Customer>(i => i.Customer).WithMany(c => c.Invoices).HasForeignKey(i => i.CustomerId);
        }

        public void LoadCustomersFromCsv(string path)
        {
            using (var reader = new StreamReader(path))
            {
                // skip header row
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var fields = line.Split(',');

                    Console.WriteLine("Customer " + fields[0].ToString());

                    var customer = new Customer
                    {
                        EmailAddress = fields[1],
                        ContactName = fields[2],
                        CompanyName = fields[3],
                        Department = fields[4],
                        PhoneNumber = fields[5],
                        LoyaltyCard = fields[6],
                        Gender = fields[7],
                        Country = fields[8],
                        RegistrationDate = DateTime.TryParse(fields[9], out DateTime registrationDate) ? registrationDate : default,
                        LoyaltyPoints = decimal.TryParse(fields[10], out decimal loyaltyPoints) ? loyaltyPoints : default
                    };

                    // check if customer with the same ID exists
                    if (!Customers.Any(c => c.Id == int.Parse(fields[0])))
                    {
                        Customers.Add(customer);
                    }
                }
            }

            SaveChanges();
        }

        public void LoadProductsFromCsv(string path)
        {
            using (var reader = new StreamReader(path))
            {
                // skip header row
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var fields = line.Split(',');

                    Console.WriteLine("Product " + fields[0].ToString());

                    var product = new Product
                    {
                        TaxRate = int.TryParse(fields[2], out int TaxRate) ? TaxRate : default,
                        Name = fields[3],
                        Price = decimal.TryParse(fields[2], out decimal Price) ? Price : default,
                        BarCode = int.TryParse(fields[4], out int BarCode) ? BarCode : default,
                        ProductCode = int.TryParse(fields[4], out int ProductCode) ? ProductCode : default,
                    };

                    Products.Add(product);
                }
            }
            SaveChanges();
        }
    }
}
