public class Product
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public decimal Price { get; set; }

    public ICollection<Sales>? Sales { get; set; } = new List<Sales>();
}