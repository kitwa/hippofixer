
namespace API.Entities;

public class MaintenanceRequest
{
    public int Id { get; set; }
    public int TenantId { get; set; }
    public int UnitId { get; set; }
    public int ContractorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime RequestDate { get; set; }
    public int StatusId { get; set; }
    public AppUser Tenant { get; set; }
    public AppUser Contractor { get; set; }
    public Unit Unit { get; set; }
    public WorkOrderStatus Status { get; set; } 
}
