using System;
using System.Collections.Generic;

namespace Ebay_Project_PRN.Model
{
    public partial class StorePromotion
    {
        public int PromotionId { get; set; }
        public int? StoreId { get; set; }
        public string PromotionCode { get; set; } = null!;
        public decimal? DiscountPercentage { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public virtual Store? Store { get; set; }
    }
}
