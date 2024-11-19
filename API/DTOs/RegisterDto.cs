using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
        public string Username { get; set; }
        [Required]
        // [StringLength(12, MinimumLength = 6)]
        public string Password { get; set; }
        public int CityId { get; set; }
        public int GenderId { get; set; }
        public string Phone { get; set; }

    }
}