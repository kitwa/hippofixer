namespace API.Entities;

public class WorkOrderStatus
{
    public int Id { get; set; }
    public string Identifier { get; set; } 
    public string Description { get; set; } 

    // Navigation properties
    public ICollection<WorkOrder> WorkOrders { get; set; }
    public ICollection<MaintenanceRequest> MaintenanceRequests { get; set; }
}
