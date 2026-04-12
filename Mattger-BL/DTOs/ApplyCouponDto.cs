using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mattger_BL.DTOs
{
    public class ApplyCouponDto
    {
        public string Code { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
