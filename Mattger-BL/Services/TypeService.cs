using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_BL.IServices;
using Mattger_DAL.Entities;
using Mattger_DAL.IRepos;

namespace Mattger_BL.Services
{
    public class TypeService : ITypeService
    {
        private readonly IGenericRepo<ProductType> _repo;

        public TypeService(IGenericRepo<ProductType> repo)
        {
            _repo = repo;
        }

        public IEnumerable<ProductType> GetAll()
        {
            return _repo.GetAll();
        }

        public ProductType GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Add(ProductType productType)
        {
            _repo.Add(productType);
            _repo.Save();
        }

        public void Update(int TypeId,ProductType productType)
        {
            var type = _repo.GetById(TypeId);
            type.Name = productType.Name;
            _repo.Update(type);
            _repo.Save();
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }
    }
}
