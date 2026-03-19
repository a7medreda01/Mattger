using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mattger_BL.DTOs
{
    public class AddCartItemDTO
    {
        public string UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
