using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Mattger_BL.DTOs;
using Mattger_DAL.Entities;

namespace Mattger_BL.IServices
{
    public interface IProductService
    {
        //IEnumerable<Product> GetAll();
        //IEnumerable<Product> GetAll(string search);
        //IEnumerable<Product> GetAll(int? cat);
        //Task<(List<Product> Items, int TotalItems)> GetProductsAsync(
        //        int page, int pageSize, int? category = null, string? search = null);
        Task<Product> GetByIdAsync(int id);

        void Add(CreateProductDTO product);

        void Update(int id,UpdateProductDTO product);

        void Delete(int id);
        Task<List<Product>> GetAllProductsAsync(ProductQueryParams param);

    }
}
