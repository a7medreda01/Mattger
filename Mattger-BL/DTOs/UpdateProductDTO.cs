using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Mattger_BL.DTOs
{
    public class UpdateProductDTO
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? Details { get; set; }

        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }

        public int StockQuantity { get; set; }

        public decimal? Discount { get; set; }

        public int ProductBrandId { get; set; }
        public int ProductTypeId { get; set; }

        public List<IFormFile>? Images { get; set; }
    }
}
