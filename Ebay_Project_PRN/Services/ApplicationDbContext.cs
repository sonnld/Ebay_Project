using Ebay_Project_PRN.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ebay_Project_PRN.Services
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var client = new IdentityRole("client");
            admin.NormalizedName = "client";

            var seller = new IdentityRole("seller");
            admin.NormalizedName = "seller";

            modelBuilder.Entity<IdentityRole>().HasData(admin, client, seller);
        }
    }
}
