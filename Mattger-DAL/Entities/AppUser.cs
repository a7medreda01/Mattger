using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Mattger_DAL.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }

        public ICollection<Order> Orders { get; set; } = default!;
    }
}
