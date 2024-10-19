using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Phone { get; set; }
        public int GenderId { get; set; }
        public Gender Gender {get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoPublicId { get; set; }
        public string Twitter { get; set; }
        public string Youtube { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public ICollection<Property> Properties { get; set; } // For Property Managers
        public ICollection<WorkOrder> WorkOrders { get; set; } // For Contractors
        public ICollection<Unit> Units { get; set; } // For Tenants
        public ICollection<Message> MessagesSent {get; set; }
        public ICollection<Message> MessagesReceived {get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public bool Deleted { get; set; }

    }
}