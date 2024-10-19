namespace  API.Entities;

public class Payment
{
    public int Id { get; set; }
    public int TenantId { get; set; }
    public int UnitId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; }
    public AppUser Tenant { get; set; }
    public Unit Unit { get; set; }
}
