using API.Entities;

namespace API.DTOs
{
    public class QuoteDto
    {    
        public int Id { get; set; }
        public int WorkOrderId { get; set; } 
        public DateTime DateIssued { get; set; }
        public int StatusId { get; set; } 
        public WorkOrderDto WorkOrder { get; set; }
        public WorkOrderStatusDto Status { get; set; }
        public ICollection<QuoteItem> QuoteItems { get; set; } 
    }
}
