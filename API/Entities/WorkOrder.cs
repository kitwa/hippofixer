namespace  API.Entities;

public class WorkOrder
{
    public int Id { get; set; }
    public int IssueId { get; set; }
    public int PropertyId { get; set; }
    public int StatusId { get; set; }
    public int? UnitId { get; set; }    
    public int? QuoteId { get; set; } 
    public string Tittle { get; set; }
    public string Description { get; set; }
    public int ContractorId { get; set; } 
    public DateTime Created { get; set; }
    public DateTime? DateCompleted { get; set; }
    public decimal? Cost { get; set; }
    public bool QuoteRequired { get; set; }
    public bool Emergency { get; set; }
    public string CancelledNote { get; set; }
    public Property Property { get; set; }
    public WorkOrderStatus Status { get; set; } 
    public Unit Unit { get; set; }
    public AppUser Contractor { get; set; }
    public ICollection<Quote> Quotes { get; set; }
    public bool Deleted { get; set; }
}
