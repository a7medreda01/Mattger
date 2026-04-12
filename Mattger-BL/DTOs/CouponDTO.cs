using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_DAL.Entities.Enums;

namespace Mattger_BL.DTOs
{
    public class CouponDTO
    {
        public string Code { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DiscountType DiscountType { get; set; }
        public int Discount { get; set; }
    }
}
