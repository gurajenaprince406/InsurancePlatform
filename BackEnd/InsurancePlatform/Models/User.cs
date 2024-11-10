// Models/User.cs
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsurancePlatform.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(10)]
        public string Title { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Surname { get; set; }

        [Required, EmailAddress]
        public string PrimaryEmail { get; set; } // Main email address

        [Required, DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        // Navigation properties
        public List<Address> Addresses { get; set; } = new List<Address>();
        public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public List<Email> Emails { get; set; } = new List<Email>(); // Additional emails
    }
}
