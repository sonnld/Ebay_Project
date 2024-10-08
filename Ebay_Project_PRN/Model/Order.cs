using System;
using System.Collections.Generic;

namespace Ebay_Project_PRN.Model
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
            OrderLogs = new HashSet<OrderLog>();
        }

        public int OrderId { get; set; }
        public string? UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual AspNetUser? User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<OrderLog> OrderLogs { get; set; }
    }
}
