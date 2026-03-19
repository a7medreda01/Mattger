using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mattger_DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public AppUser User { get; set; } = default!;

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = default!;
        public string Address { get; set; }
        public string Phone { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = default!;
    }
}
