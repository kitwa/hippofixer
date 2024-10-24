
namespace API.Entities;

public class Issue
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? ContractorId { get; set; }
    public string Description { get; set; }
    public int IssueTypeId { get; set; }
    public int StatusId { get; set; }
    public string PhotoUrl { get; set; }
    public string PhotoPublicId { get; set; }
    public AppUser User { get; set; }
    public AppUser Contractor { get; set; }
    public WorkOrderStatus Status { get; set; } 
    public IssueType IssueType { get; set; } 
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
