using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class InvoiceUpdateDto
    {
        public DateTime DateSubmitted { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DatePaid { get; set; }

    }
}
