using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firstcartproduct
    {
        public decimal Id { get; set; }
        public decimal? CartId { get; set; }
        public decimal? ProductId { get; set; }
        public decimal? CartQuantity { get; set; }
        public virtual Firstcart Cart { get; set; }
        public virtual Firstproduct Product { get; set; }
    }
}
