using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firstbank
    {
        public Firstbank()
        {
            Firstpayments = new HashSet<Firstpayment>();
        }

        public decimal Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal? Balance { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }

        public virtual ICollection<Firstpayment> Firstpayments { get; set; }
    }
}
