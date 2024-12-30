namespace API.DTOs
{
    public class EmailInvoiceDto
    {
        public string FileName { get; set; }
        public string Email { get; set; }
        public string InvoiceId { get; set; }
        public string InvoiceLink { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public IFormFile File { get; set; }
    }
}