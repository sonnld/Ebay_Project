using System;
using System.Collections.Generic;

namespace Ebay_Project_PRN.Model
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public int? StoreId { get; set; }
        public string CategoryName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Store? Store { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
