using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mattger_BL.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? Details { get; set; }

        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }

        public int? Quantity { get; set; }
        public decimal? Rating { get; set; }
        public int? Sale { get; set; }

        public int StockQuantity { get; set; }

        public decimal? Discount { get; set; }

        public DateTime? FlashStartDate { get; set; }
        public DateTime? FlashEndDate { get; set; }

        public int? SalesCount { get; set; }

        public string Status { get; set; } = default!;

        // Relations (display only)
        public string BrandName { get; set; } = default!;
        public string TypeName { get; set; } = default!;

        public List<string>? Images { get; set; }
        public List<ProductReviewDTO>? Reviews { get; set; }

    }
}

