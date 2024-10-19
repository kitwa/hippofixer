namespace  API.Entities;

public class InvoiceItem
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; } 
    public int Quantity { get; set; } 
    public Invoice Invoice { get; set; }
}
