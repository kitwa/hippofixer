using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class IssueDto
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoPublicId { get; set; }
        public bool Emergency { get; set; }
        public MemberDto Client { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int IssueTypeId { get; set; }
        public IssueType IssueType { get; set; }
        public int StatusId { get; set; }
        public WorkOrderStatusDto Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
    }
}
