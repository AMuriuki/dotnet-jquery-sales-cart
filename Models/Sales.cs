public class Sales
{
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public decimal Price { get; set; }
}