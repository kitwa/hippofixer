using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class SearchUnitDto
    {
        public int? PropertyId { get; set; }
        public string UnitNumber { get; set; }
        public int? TenantId { get; set; }

    }
}
