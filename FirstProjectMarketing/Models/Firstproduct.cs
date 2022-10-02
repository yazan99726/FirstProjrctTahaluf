using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firstproduct
    {
        public Firstproduct()
        {
            Firstdiscounts = new HashSet<Firstdiscount>();
            Firstorderproducts = new HashSet<Firstorderproduct>();
        }

        public decimal ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDicription { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductWholesaleprice { get; set; }
        public string ProductImage1 { get; set; }
        public string ProductImage2 { get; set; }
        public string ProductImage3 { get; set; }
        public decimal? ProductQuantity { get; set; }
        public decimal? CatigoryId { get; set; }

        public virtual Firstcatigory Catigory { get; set; }
        public virtual ICollection<Firstdiscount> Firstdiscounts { get; set; }
        public virtual ICollection<Firstorderproduct> Firstorderproducts { get; set; }

        [NotMapped]
        public virtual IFormFile imgfile1 { get; set; }
        [NotMapped]
        public virtual IFormFile imgfile2 { get; set; }
        [NotMapped]
        public virtual IFormFile imgfile3 { get; set; }
    }
}
