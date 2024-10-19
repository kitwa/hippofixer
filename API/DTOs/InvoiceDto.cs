namespace API.DTOs
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }
        public int WorkOrderId { get; set; }
        public WorkOrderDto WorkOrder { get; set; }
        public int ContractorId { get; set; }
        public MemberDto Contrator { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateSubmitted { get; set; }
        public DateTime? DatePaid { get; set; }
    }
}
