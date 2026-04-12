using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Mattger_DAL.Entities.Enums;

namespace Mattger_DAL.Entities
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DiscountType DiscountType { get; set; }
        public int Discount { get; set; }
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();


    }
}
