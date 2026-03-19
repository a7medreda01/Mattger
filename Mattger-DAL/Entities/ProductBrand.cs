using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mattger_DAL.Entities
{
    public class ProductBrand
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;


        public ICollection<Product> Products { get; set; } = default!;
    }
}
