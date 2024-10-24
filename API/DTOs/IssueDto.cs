using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class IssueDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? ContractorId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int StatusId { get; set; }
        public int IssueTypeId { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoPublicId { get; set; }
        public MemberDto User { get; set; }
        public MemberDto Contractor { get; set; }
        public WorkOrderStatusDto Status { get; set; } 
        public IssueType IssueType { get; set; } 
    }
}
