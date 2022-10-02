using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firststore
    {
        public Firststore()
        {
            Firstcatigories = new HashSet<Firstcatigory>();
        }

        public decimal StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreDiscription { get; set; }
        public decimal? StoreFloorNumber { get; set; }

        public virtual ICollection<Firstcatigory> Firstcatigories { get; set; }
    }
}
