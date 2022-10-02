using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firsthomeimage
    {
        public decimal Id { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public virtual IFormFile imgfile { get; set; }
    }
}
