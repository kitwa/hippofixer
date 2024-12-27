using API.Entities;

namespace API.DTOs
{
    public class InvoiceItemDto
    {    
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } 
        public int Quantity { get; set; } 
        public bool Deleted { get; set; }
    }
}
