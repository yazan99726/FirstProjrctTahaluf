using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firstlogin
    {
        public decimal LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal? Userid { get; set; }
        public decimal? RoleId { get; set; }

        public virtual Firstrole Role { get; set; }
        public virtual Firstuser User { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirm Password required")]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }
    }
}
