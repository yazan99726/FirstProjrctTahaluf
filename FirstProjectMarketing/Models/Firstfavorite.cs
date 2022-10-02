using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firstfavorite
    {
        public decimal Id { get; set; }
        public decimal? UserId { get; set; }
        public decimal? ProductId { get; set; }

        public virtual Firstproduct Product { get; set; }
        public virtual Firstuser User { get; set; }
    }
}
