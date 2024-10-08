using System;
using System.Collections.Generic;

namespace Ebay_Project_PRN.Model
{
    public partial class Store
    {
        public Store()
        {
            Categories = new HashSet<Category>();
            Products = new HashSet<Product>();
            StorePromotions = new HashSet<StorePromotion>();
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; } = null!;
        public string? OwnerId { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual AspNetUser? Owner { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<StorePromotion> StorePromotions { get; set; }
    }
}
