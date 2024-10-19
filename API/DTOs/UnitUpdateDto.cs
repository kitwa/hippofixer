using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class UnitUpdateDto
    {
        public int PropertyId { get; set; }
        public string UnitNumber { get; set; }
        public int? TenantId { get; set; }
        public MemberDto Tenant { get; set; }
        public decimal MonthlyRent { get; set; }
        public DateTime? LeaseStartDate { get; set; }
        public DateTime? LeaseEndDate { get; set; }
        public List<WorkOrderDto> WorkOrders { get; set; }

    }
}
