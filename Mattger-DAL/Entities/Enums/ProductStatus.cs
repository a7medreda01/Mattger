using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mattger_DAL.Entities.Enums
{
    public enum ProductStatus
    {
        Active = 1,     // ظاهر للبيع
        Inactive = 2,   // مخفي
        OutOfStock = 3  // خلصان
    }
}
