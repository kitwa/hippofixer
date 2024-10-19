using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class UnitDto
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string UnitNumber { get; set; }
        public int? TenantId { get; set; }
        public MemberDto Tenant { get; set; } // Full name of Tenant
        public decimal MonthlyRent { get; set; }
        public DateTime? LeaseStartDate { get; set; }
        public DateTime? LeaseEndDate { get; set; }
        public List<WorkOrderDto> WorkOrders { get; set; }
    }
}
