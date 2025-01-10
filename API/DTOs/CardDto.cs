using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CardDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CardHolderName { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        public string BankName { get; set; }
        [Required]
        public string CardType { get; set; }
        public string Cvv { get; set; }
        public bool IsDefault { get; set; } = false;
        public int ContractorId { get; set; }
        public MemberDto Contractor { get; set; }
    }
}
