using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CardDto
    {
        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string BankName { get; set; }

        [Required]
        public string CardType { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        public bool IsDefault { get; set; } = false;
    }
}
