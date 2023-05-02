public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? sku { get; set; }
    public string? imageUrl { get; set; }
    public ICollection<SaleProduct>? SaleProducts { get; set; } = new List<SaleProduct>();
}
public class Sale
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public ICollection<SaleProduct>? SaleProducts { get; set; }
}

public class SaleProduct
{
    public int SaleId { get; set; }
    public Sale? Sale { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}