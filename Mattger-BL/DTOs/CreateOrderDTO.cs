using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mattger_BL.DTOs
{
    namespace Mattger_BL.DTOs
    {
        public class CreateOrderDTO
        {
            public string UserId { get; set; }

            public int CartId { get; set; }

            public string Address { get; set; }

            public string Phone { get; set; }
            public string? FullName { get; set; }

        }
    }
}
