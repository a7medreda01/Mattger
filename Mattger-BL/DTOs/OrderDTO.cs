using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_DAL.Entities;

namespace Mattger_BL.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? FullName { get; set; }

        public ICollection<OrderItemDTO> OrderItems { get; set; } = default!;

    }
}
