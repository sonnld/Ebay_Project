using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Ebay_Project_PRN.Model
{
    public partial class AspNetUser : IdentityUser
    {
        public AspNetUser()
        {

            OrderLogs = new HashSet<OrderLog>();
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            ShoppingCarts = new HashSet<ShoppingCart>();
            Stores = new HashSet<Store>();
            UserActivityLogs = new HashSet<UserActivityLog>();
        }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        //public string Id { get; set; } = null!;
        //public string FirstName { get; set; } = null!;
        //public string LastName { get; set; } = null!;
        //public string Address { get; set; } = null!;
        //public DateTime CreatedAt { get; set; }
        //public string? UserName { get; set; }
        //public string? NormalizedUserName { get; set; }
        //public string? Email { get; set; }
        //public string? NormalizedEmail { get; set; }
        //public bool EmailConfirmed { get; set; }
        //public string? PasswordHash { get; set; }
        //public string? SecurityStamp { get; set; }
        //public string? ConcurrencyStamp { get; set; }
        //public string? PhoneNumber { get; set; }
        //public bool PhoneNumberConfirmed { get; set; }
        //public bool TwoFactorEnabled { get; set; }
        //public DateTimeOffset? LockoutEnd { get; set; }
        //public bool LockoutEnabled { get; set; }
        //public int AccessFailedCount { get; set; }


        public virtual ICollection<OrderLog> OrderLogs { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
        public virtual ICollection<UserActivityLog> UserActivityLogs { get; set; }

    }
}
