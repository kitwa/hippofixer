namespace  API.Entities;

public class Invoice
{
    public int Id { get; set; }
    public int WorkOrderId { get; set; }
    public int ContractorId { get; set; }
    public decimal TotalAmount { get; set; }
    public ICollection<InvoiceItem> InvoiceItems { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? DatePaid { get; set; }
    public WorkOrder WorkOrder { get; set; }
    public AppUser Contractor { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public bool Deleted { get; set; }
}
