using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firstorder
    {
        public Firstorder()
        {
            Firstorderproducts = new HashSet<Firstorderproduct>();
        }

        public decimal OrderId { get; set; }
        public DateTime? OrderPaydate { get; set; }
        public decimal? Totalprice { get; set; }
        public decimal? UserId { get; set; }
        public decimal? PaymentId { get; set; }

        public virtual Firstpayment Payment { get; set; }
        public virtual Firstuser User { get; set; }
        public virtual ICollection<Firstorderproduct> Firstorderproducts { get; set; }
    }
}
