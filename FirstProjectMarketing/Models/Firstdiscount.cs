using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firstdiscount
    {
        public decimal DiscountId { get; set; }
        public DateTime DiscountStartdate { get; set; }
        public DateTime DiscountEnddate { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? Productid { get; set; }

        public virtual Firstproduct Product { get; set; }
    }
}
