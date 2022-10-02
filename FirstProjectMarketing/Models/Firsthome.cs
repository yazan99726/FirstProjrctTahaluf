using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firsthome
    {
        public decimal Id { get; set; }
        public string Projectname { get; set; }
        public string Logo { get; set; }
        public string Discription { get; set; }
        [NotMapped]
        public virtual IFormFile imgfile { get; set; }
    }
}
