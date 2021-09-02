using HMS.Models.Administration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Test1.Models;
using Test1.Models.EVouchers;
using Test1.Models.Payments;

namespace Test1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Evoucher> EVouchers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<PurchaseByUser> PurchaseByUsers { get; set; }
    }
}