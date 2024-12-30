using API.Entities;

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullAddress { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public int GenderId { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public decimal? Vat { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoPublicId { get; set; }
        public string Twitter { get; set; }
        public string Youtube { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public int? PropertyId { get; set; } // Only for Property Managers and Contractors

    }
}