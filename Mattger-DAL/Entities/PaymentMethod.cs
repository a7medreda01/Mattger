using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mattger_DAL.Entities
{
    public class PaymentMethod
    {
        public int Id { get; set; }

        // FK
        public string UserId { get; set; }
        public AppUser User { get; set; }

        // Card Info
        public string CardType { get; set; } // Visa, MasterCard
        public string CardName { get; set; }
        public string CardNumber { get; set; } // مهم يكون string
        public string ExpiryDate { get; set; } // MM/YY

    }
}
