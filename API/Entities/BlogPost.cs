using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoPublicId { get; set; }
        public AppUser appUser  { get; set; }
        public int AppUserId { get; set; }
        public DateTime CreatedDate { get; set; } 
        public bool Deleted { get; set; }
    }
}