using API.Entities;

namespace API.DTOs
{
    public class MemberCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullAddress { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public int GenderId { get; set; }
        public int CityId { get; set; }
        public int? PropertyId { get; set; } // Only for Property Managers and Contractors
    }
}