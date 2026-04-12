using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_DAL.Entities;

namespace Mattger_BL.DTOs
{
    public class WishlistItemDTO
    {
        public string UserId { get; set; }

        public int WishlistId { get; set; }

        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
    }
}
