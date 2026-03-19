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

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string PictureUrl { get; set; }

        public string BrandName { get; set; }

        public string TypeName { get; set; }
    }
}
