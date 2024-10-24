using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class WorkOrderDto
    {
        public int Id { get; set; }        
        public int IssueId { get; set; }
        public int PropertyId { get; set; }
        public int StatusId { get; set; }
        public int? UnitId { get; set; }
        public int? QuoteId { get; set; } 
        public int ContractorId { get; set; } 
        public string Tittle { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? DateCompleted { get; set; }
        public decimal? Cost { get; set; }
        public bool QuoteRequired { get; set; }
        public bool Emergency { get; set; }
        public string CancelledNote { get; set; }
        public PropertyDto Property { get; set; }
        public WorkOrderStatusDto Status { get; set; } 
        public UnitDto Unit { get; set; }
        public List<QuoteDto> Quotes { get; set; }
        public MemberDto Contractor { get; set; }
    }
}
