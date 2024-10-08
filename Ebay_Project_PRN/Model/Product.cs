using System;
using System.Collections.Generic;

namespace Ebay_Project_PRN.Model
{
    public partial class Product
    {
        public Product()
        {
            CartItems = new HashSet<CartItem>();
            OrderItems = new HashSet<OrderItem>();
            ProductImages = new HashSet<ProductImage>();
            Reviews = new HashSet<Review>();
            UserActivityLogs = new HashSet<UserActivityLog>();
        }

        public int ProductId { get; set; }
        public int? StoreId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Store? Store { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<UserActivityLog> UserActivityLogs { get; set; }
    }
}
