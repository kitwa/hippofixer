using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class BlogPostUpdateDto
    {
        public string Title { get; set; }
        public int AppUserId { get; set; }
        public string Content { get; set; }
        public MemberDto appUser {get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
