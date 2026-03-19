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
    public class BrandService : IBrandService
    {
        private readonly IGenericRepo<ProductBrand> _repo;

        public BrandService(IGenericRepo<ProductBrand> repo)
        {
            _repo = repo;
        }

        public IEnumerable<ProductBrand> GetAll()
        {
            return _repo.GetAll();
        }

        public ProductBrand GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Add(ProductBrand productBrand)
        {
            _repo.Add(productBrand);
            _repo.Save();
        }

        public void Update(ProductBrand productBrand)
        {
            _repo.Update(productBrand);
            _repo.Save();
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }
    }
}
