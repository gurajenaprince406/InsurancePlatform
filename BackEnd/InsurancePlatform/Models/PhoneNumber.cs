
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsurancePlatform.Models
{
    public class PhoneNumber
    {
        [Key]
        public int PhoneNumberId { get; set; }

        [Required, Phone]
        public string Number { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; } // Navigation property
    }
}

