using System;
using System.Collections.Generic;

namespace Ebay_Project_PRN.Model
{
    public partial class OrderLog
    {
        public int LogId { get; set; }
        public int OrderId { get; set; }
        public string? UserId { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public int? AddressId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual AspNetUser? User { get; set; }
    }
}
