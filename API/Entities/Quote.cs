namespace  API.Entities;

public class Quote
{
    public int Id { get; set; }
    public int WorkOrderId { get; set; } 
    public DateTime DateIssued { get; set; }
    public int StatusId { get; set; } 
    public int? InvoiceId { get; set; }
    // Navigation properties
    public WorkOrder WorkOrder { get; set; }
    public WorkOrderStatus Status { get; set; }
    public ICollection<QuoteItem> QuoteItems { get; set; }
    public Invoice Invoice { get; set; }
    public bool Deleted { get; set; }
}
