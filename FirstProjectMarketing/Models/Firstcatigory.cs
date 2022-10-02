using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firstcatigory
    {
        public Firstcatigory()
        {
            Firstproducts = new HashSet<Firstproduct>();
        }

        public decimal CatigoryId { get; set; }
        public string CatigoryName { get; set; }
        public string Discription { get; set; }
        public decimal? StoreId { get; set; }

        public virtual Firststore Store { get; set; }
        public virtual ICollection<Firstproduct> Firstproducts { get; set; }
    }
}
