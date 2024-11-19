using API.Entities;

namespace API.DTOs
{
    public class WorkOrderDto
    {
        public int Id { get; set; }
        public decimal? Cost { get; set; }
        public string CancelledNote { get; set; }
        public int IssueId { get; set; }
        public IssueDto Issue { get; set; }
        public int ContractorId { get; set; }
        public MemberDto Contractor { get; set; }
        public int StatusId { get; set; }
        public WorkOrderStatusDto Status { get; set; }
        public ICollection<InvoiceDto> Invoices { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
