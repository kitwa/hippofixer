using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int PropertyManagerId { get; set; }
        public MemberDto PropertyManager { get; set; } 
        public bool Deleted { get; set; }
    }
}
