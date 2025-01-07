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
        public string MaskedCardNumber { get; set; } // Only store masked number, e.g., "**** **** **** 1234"

        [Required]
        [StringLength(50)]
        public string BankName { get; set; } // Bank associated with the card

        [Required]
        [StringLength(20)]
        public string CardType { get; set; } // e.g., Visa, MasterCard

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; } // Expiry date

        [Required]
        public int AppUserId { get; set; } // Foreign key for AppUser

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; } // Navigation property to AppUser

        public DateTime AddedDate { get; set; } = DateTime.UtcNow; // Date card was added

        public bool IsDefault { get; set; } // Indicates if this is the default card
    }
}
