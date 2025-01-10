using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string CardHolderName { get; set; }

        [Required]
        [StringLength(50)]
        public string CardNumber { get; set; } // Only store masked number, e.g., "**** **** **** 1234"

        [Required]
        [StringLength(50)]
        public string BankName { get; set; }

        [Required]
        [StringLength(20)]
        public string CardType { get; set; } 

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; } 

        public DateTime AddedDate { get; set; } = DateTime.UtcNow; 
        public string Cvv { get; set; }
        public bool IsDefault { get; set; } 

        [Required]
        public int AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }
    }
}
