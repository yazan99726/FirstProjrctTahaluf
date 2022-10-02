using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firstpayment
    {
        public Firstpayment()
        {
            Firstorders = new HashSet<Firstorder>();
        }

        public decimal Id { get; set; }
        public string Cardnumber { get; set; }
        public string Nameoncard { get; set; }
        public decimal Cvc { get; set; }
        public DateTime Expirydate { get; set; }
        public string Paymenttype { get; set; }
        public decimal? BankId { get; set; }

        public virtual Firstbank Bank { get; set; }
        public virtual ICollection<Firstorder> Firstorders { get; set; }
    }
}
