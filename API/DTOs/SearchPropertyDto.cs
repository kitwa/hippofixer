using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class SearchPropertyDto
    {
        public string Address { get; set; }
        public int? PropertyManagerId { get; set; }
        public string PropertyManagerName { get; set; } 
    }
}
