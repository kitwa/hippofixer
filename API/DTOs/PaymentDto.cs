namespace API.DTOs
{
    public class PaymentDto
        {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public MemberDto TenantN { get; set; }
        public int UnitId { get; set; }
        public UnitDto Unit{ get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } // e.g., "Credit Card", "Bank Transfer"
    }
}
