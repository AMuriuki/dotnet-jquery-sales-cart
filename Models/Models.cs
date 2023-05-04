public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? imageUrl { get; set; }
    public ICollection<SoldProduct>? SoldProducts { get; set; } = new List<SoldProduct>();
}
public class Invoice
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int CustomerId { get; set; }
    public int TaxPercent { get; set; }
    public decimal TaxValue { get; set; }
    public int DiscountPercent { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal Shipping { get; set; }
    public decimal GrandTotal { get; set; }
    public Customer? Customer { get; set; }
    public ICollection<SoldProduct>? SoldProducts { get; set; }
}

public class SoldProduct
{
    public int InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
}

public static class SoldProductExtensions
{
    public static string ProductName(this SoldProduct soldProduct)
    {
        return soldProduct.Product?.Name ?? "Product not found";
    }
}

public class Customer
{
    public int Id { get; set; }
    public string? fname { get; set; }
    public string? lname { get; set; }
    public ICollection<Invoice>? Invoices { get; set; }
}