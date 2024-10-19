using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AppUserId { get; set; }
        public string Content { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoPublicId { get; set; }
        public MemberDto appUser {get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
