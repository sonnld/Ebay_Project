using System;
using System.Collections.Generic;

namespace Ebay_Project_PRN.Model
{
    public partial class UserActivityLog
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int ProductId { get; set; }
        public DateTime? ViewedAt { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual AspNetUser User { get; set; } = null!;
    }
}
