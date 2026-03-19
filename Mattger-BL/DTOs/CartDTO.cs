using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mattger_BL.DTOs
{
    public class CartDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public List<CartItemDTO> Items { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
