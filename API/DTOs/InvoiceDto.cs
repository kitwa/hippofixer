namespace API.DTOs
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public WorkOrderDto WorkOrder { get; set; }
        public int ContractorId { get; set; }
        public MemberDto Contractor { get; set; }
        public decimal Amount { get; set; }
        public ICollection<InvoiceItemDto> InvoiceItems { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DatePaid { get; set; }
    }
}
