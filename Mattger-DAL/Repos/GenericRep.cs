using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Mattger_DAL.Data;
using Mattger_DAL.Entities;
using Mattger_DAL.IRepos;
using Microsoft.EntityFrameworkCore;

namespace Mattger_DAL.Repos
{
    public class GenericRep<TEntity> : IGenericRepo<TEntity> where TEntity : class
    {
        private readonly MattgerDBContext _dBContext;
        private readonly DbSet<TEntity> _dbSet;
        public GenericRep(MattgerDBContext dBContext)
        {
            this._dBContext = dBContext;
            _dbSet= _dBContext.Set<TEntity>();
        }
        void IGenericRepo<TEntity>.Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        IEnumerable<TEntity> IGenericRepo<TEntity>.GetAll()
        {
            return _dbSet.ToList();
        }
        //search
        IEnumerable<TEntity> IGenericRepo<TEntity>.GetAll(Expression<Func<TEntity, bool>> search, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.Where(search).ToList();
        }
        //category
        public IEnumerable<TEntity> GetAllByCategory(Expression<Func<TEntity, bool>> cat, params Expression<Func<TEntity, object>>[] includes)

        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.Where(cat).ToList();
        }
        //include
        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }
        //paging
        public IQueryable<TEntity> GetAll(
    Expression<Func<TEntity, bool>>? filter = null,
    int? skip = null,
    int? take = null,
    params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            // Filter
            if (filter != null)
                query = query.Where(filter);

            // Includes
            if (includes != null && includes.Any())
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            // Pagination
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);

            return query;
        }
        TEntity IGenericRepo<TEntity>.GetById(int id)
        {
            return _dbSet.Find(id);
        }

        void IGenericRepo<TEntity>.Save()
        {
            _dBContext.SaveChanges();
        }

        void IGenericRepo<TEntity>.Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
        void IGenericRepo<TEntity>.Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);

        }
        //مؤقتا
        public Cart GetCart(string userId)
        {
            return _dBContext.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(p=>p.Images)
                .FirstOrDefault(c => c.UserId == userId);
        }
        public Wishlist GetWishlist(string userId)
        {
            return _dBContext.Wishlists
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .ThenInclude(i=>i.Images)
                .FirstOrDefault(c => c.UserId == userId);
        }
        //مؤقتا
        public ICollection<Order> GetOrdersByUser(string userId)
        {
            return _dBContext.Orders
                .Include(c => c.OrderItems)
                .ThenInclude(i => i.Product)
                .Where(c => c.UserId == userId).ToList();
        }
        public Order GetOrderById(int id)
        {
            return _dBContext.Orders
                .Include(c=>c.User)
                .Include(c => c.OrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(o=>o.Id==id);
        }
        //getall products
        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            return await _dBContext.Products
                .AsNoTracking()
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .Include(p => p.Images)
                .Include(p => p.Reviews)
                .ToListAsync();
        }
        public async Task<Product> GetProductAsync(int id)
        {
            return await _dBContext.Products
                .AsNoTracking()
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .Include(p => p.Images)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id==id);
        }
    }
}
