using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firstuser
    {
        public Firstuser()
        {
            Firstcarts = new HashSet<Firstcart>();
            Firstlogins = new HashSet<Firstlogin>();
            Firstorders = new HashSet<Firstorder>();
            Firsttestimonials = new HashSet<Firsttestimonial>();
        }

        public decimal UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public virtual ICollection<Firstcart> Firstcarts { get; set; }
        public virtual ICollection<Firstlogin> Firstlogins { get; set; }
        public virtual ICollection<Firstorder> Firstorders { get; set; }
        public virtual ICollection<Firsttestimonial> Firsttestimonials { get; set; }

        [NotMapped]
        public virtual IFormFile imgfile { get; set; }
    }
}
