using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firstorderproduct
    {
        public decimal Id { get; set; }
        public decimal? ProductId { get; set; }
        public decimal? OrderId { get; set; }
        public decimal Quantity { get; set; }

        public virtual Firstorder Order { get; set; }
        public virtual Firstproduct Product { get; set; }
    }
}
