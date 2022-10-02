using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firstcart
    {
        public decimal CartId { get; set; }
        
        public decimal? UserId { get; set; }

        public virtual Firstuser User { get; set; }
    }
}
