using System;
using System.Collections.Generic;

namespace Ebay_Project_PRN.Model
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int? ProductId { get; set; }
        public string? UserId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Product? Product { get; set; }
        public virtual AspNetUser? User { get; set; }
    }
}
