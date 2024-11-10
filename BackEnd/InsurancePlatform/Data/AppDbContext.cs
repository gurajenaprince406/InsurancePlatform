// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using InsurancePlatform.Models;

namespace InsurancePlatform.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Quote> Quotes { get; set; }
        public DbSet<MotorRisk> MotorRisks { get; set; } // Add this line
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}


