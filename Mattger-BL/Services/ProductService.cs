using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mattger_BL.Services
{
    using Mattger_BL.IServices;
    using Mattger_DAL.Entities;
    using Mattger_DAL.IRepos;

    public class ProductService : IProductService
    {
        private readonly IGenericRepo<Product> _repo;

        public ProductService(IGenericRepo<Product> repo)
        {
            _repo = repo;
        }
        //include
        public IEnumerable<Product> GetAll()
        {
            return _repo.GetAll(p=>p.ProductBrand,p=>p.ProductType);
        }
        //search
        public IEnumerable<Product> GetAll(string search)
        {
            return _repo.GetAll(p => p.Name.Contains(search), [p => p.ProductBrand, p => p.ProductType]);
        }
        //category
        public IEnumerable<Product> GetAll(int? catId)
        {
            return _repo.GetAllByCategory(p => p.ProductType.Id == catId, [p => p.ProductBrand, p => p.ProductType]);
        }
        //paging
       
            public async Task<(List<Product> Items, int TotalItems)> GetProductsAsync(
                int page, int pageSize, int? category = null, string? search = null)
            {
                IQueryable<Product> query;

                // Filter
                query = _repo.GetAll(
                    filter: p =>
                        (!category.HasValue || p.ProductType.Id == category.Value) &&
                        (string.IsNullOrEmpty(search) || p.Name.Contains(search)),
                    skip: (page - 1) * pageSize,
                    take: pageSize,
                    p => p.ProductBrand, p => p.ProductType // Includes
                );

                var totalItems = query.Count(); // قبل pagination لو عايز العدد الكامل
                var items = query.ToList();

                return (items, totalItems);
            }
        
        public Product GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Add(Product product)
        {
            _repo.Add(product);
            _repo.Save();
        }

        public void Update(Product product)
        {
            _repo.Update(product);
            _repo.Save();
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }
    }
}
