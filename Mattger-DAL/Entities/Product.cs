using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mattger_DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } =default!;

        public string? Description { get; set; } = default!;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string? PictureUrl { get; set; } 

        public int ProductBrandId { get; set; }
        public ProductBrand? ProductBrand { get; set; } = default!;

        public int ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; } = default!;
    }
}
