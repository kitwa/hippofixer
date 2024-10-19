using API.Entities;

namespace API.DTOs
{
    public class InvoiceItemDto
    {    
        public int QuoteId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } 
        public int Quantity { get; set; } 
        public InvoiceDto Quote { get; set; }
    }
}
