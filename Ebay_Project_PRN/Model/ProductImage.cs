﻿using System;
using System.Collections.Generic;

namespace Ebay_Project_PRN.Model
{
    public partial class ProductImage
    {
        public int ImageId { get; set; }
        public int? ProductId { get; set; }
        public string ImageUrl { get; set; } = null!;

        public virtual Product? Product { get; set; }
    }
}
