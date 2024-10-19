namespace  API.Entities;

public class QuoteItem
{
    public int Id { get; set; }
    public int QuoteId { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; } 
    public int Quantity { get; set; } 
    public Quote Quote { get; set; }
}
