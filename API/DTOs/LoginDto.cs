using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class NewPasswordDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        public string ResetToken { get; set; }
    }
}