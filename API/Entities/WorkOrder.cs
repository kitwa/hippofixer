namespace  API.Entities;

public class WorkOrder
{
    public int Id { get; set; }
    public decimal? Cost { get; set; }
    public string CancelledNote { get; set; }
    public int IssueId { get; set; }
    public Issue Issue { get; set; }
    public int ContractorId { get; set; }
    public AppUser Contractor { get; set; }
    public int StatusId { get; set; }
    public WorkOrderStatus Status { get; set; }
    public ICollection<Invoice> Invoices { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DateCompleted { get; set; }
    public bool Deleted { get; set; }
}
