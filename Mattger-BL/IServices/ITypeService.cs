using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_DAL.Entities;

namespace Mattger_BL.IServices
{
    public interface ITypeService
    {
        IEnumerable<ProductType> GetAll();

        ProductType GetById(int id);

        void Add(ProductType productType);

        void Update(int TypeId, ProductType productType);

        void Delete(int id);
    }
}
