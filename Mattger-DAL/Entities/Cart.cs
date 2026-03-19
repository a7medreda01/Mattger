using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mattger_DAL.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public AppUser User { get; set; } = default!;

        public ICollection<CartItem> Items { get; set; } = default!;

    }
}
