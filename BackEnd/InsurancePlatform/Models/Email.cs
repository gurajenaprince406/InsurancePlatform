using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsurancePlatform.Models
{
    public class Email
    {
        [Key]
        public int EmailId { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; } // Navigation property
    }
}
