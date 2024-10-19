namespace  API.Entities;

public class Unit
{
    public int Id { get; set; }
    public int PropertyId { get; set; } 
    public string UnitNumber { get; set; }
    public int? TenantId { get; set; } 
    public decimal MonthlyRent { get; set; }
    public DateTime? LeaseStartDate { get; set; }
    public DateTime? LeaseEndDate { get; set; }
    public Property Property { get; set; }
    public AppUser Tenant { get; set; }        
    public ICollection<Photo> Photos {get; set; }
    public ICollection<WorkOrder> WorkOrders { get; set; }
    public DateTime Created { get; set; }
    public bool Deleted { get; set; }
}
