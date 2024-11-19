
namespace API.Entities;

public class Issue
{
    public int Id { get; set; }
    public int? ClientId { get; set; }
    public string ClientPhone { get; set; }
    public string ClientEmail { get; set; }
    public string Description { get; set; }
    public int IssueTypeId { get; set; }
    public string PhotoUrl { get; set; }
    public string PhotoPublicId { get; set; }
    public bool Emergency { get; set; }
    public AppUser Client { get; set; }
    public int CityId { get; set; }    
    public City City { get; set; }
    public IssueType IssueType { get; set; } 
    public int StatusId { get; set; }
    public WorkOrderStatus Status { get; set; } 
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; }
}
