using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_DAL.Entities;

namespace Mattger_BL.IServices
{
    public interface IBrandService
    {
        IEnumerable<ProductBrand> GetAll();

        ProductBrand GetById(int id);

        void Add(ProductBrand productBrand);

        void Update(ProductBrand productBrand);

        void Delete(int id);
    }
}
