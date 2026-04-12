using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_DAL.Entities.Enums;

namespace Mattger_DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } =default!;

        public string? Description { get; set; } = default!;
        public string? Details { get; set; } = default!;

        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int? Quantity { get; set; } = 1;
        public decimal? Rating { get; set; } = 1;
        public int? Sale { get; set; } = 1;

        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public decimal? Discount { get; set; }
        public DateTime? FlashEndDate { get; set; }  
        public DateTime? FlashStartDate { get; set; }
        public int? SalesCount { get; set; } = 0;
        public ProductStatus Status { get; set; } = ProductStatus.Active;
        public ICollection<ProductImages>? Images { get; set; }
        public ICollection<ProductReview>? Reviews { get; set; }
        public int ProductBrandId { get; set; }
        public ProductBrand? ProductBrand { get; set; } = default!;

        public int ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; } = default!;
    }
}
