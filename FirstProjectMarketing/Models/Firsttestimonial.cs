using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firsttestimonial
    {
        public decimal Id { get; set; }
        public decimal? UserId { get; set; }
        public string Discription { get; set; }
        public decimal Rating { get; set; }
        public string Status { get; set; }

        public virtual Firstuser User { get; set; }
    }
}
