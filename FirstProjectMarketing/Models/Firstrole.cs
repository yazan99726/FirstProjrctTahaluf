using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProjectMarketing.Models
{
    public partial class Firstrole
    {
        public Firstrole()
        {
            Firstlogins = new HashSet<Firstlogin>();
        }

        public decimal RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<Firstlogin> Firstlogins { get; set; }
    }
}
