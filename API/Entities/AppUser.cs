using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullAddress { get; set; }
        public string Phone { get; set; }
        public int GenderId { get; set; }
        public Gender Gender {get; set; }
        public int CityId { get; set; }
        // public decimal? Vat { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoPublicId { get; set; }
        public string Twitter { get; set; }
        public string Youtube { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; }
        public ICollection<Issue> Issues { get; set; }
        public ICollection<Message> MessagesSent {get; set; }
        public ICollection<Message> MessagesReceived {get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public bool Deleted { get; set; }

    }
}