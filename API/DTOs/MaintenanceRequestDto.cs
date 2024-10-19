using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class MaintenanceRequestDto
    {
        public int TenantId { get; set; }
        public int UnitId { get; set; }
        public int ContractorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime RequestDate { get; set; }
        public int StatusId { get; set; }
        public MemberDto Tenant { get; set; }
        public MemberDto Contractor { get; set; }
        public UnitDto Unit { get; set; }
        public WorkOrderStatusDto Status { get; set; } 
    }
}
