using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_DAL.Entities;

namespace Mattger_BL.DTOs
{
    public class WishlistDTO
    {

        public string UserId { get; set; }
        public ICollection<WishlistItemDTO> Items { get; set; } = default!;
    }
}
