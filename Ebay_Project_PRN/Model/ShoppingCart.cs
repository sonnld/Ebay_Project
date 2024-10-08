using System;
using System.Collections.Generic;

namespace Ebay_Project_PRN.Model
{
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int CartId { get; set; }
        public string? UserId { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual AspNetUser? User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
