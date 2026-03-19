using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Mattger_DAL.Entities;

namespace Mattger_DAL.IRepos
{
    public interface IGenericRepo<TEntity> where TEntity : class
    {

        //Get All
        IEnumerable<TEntity> GetAll();
        //search
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> search, params Expression<Func<TEntity, object>>[] includes);
        //include
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
        //category
        IEnumerable<TEntity> GetAllByCategory(Expression<Func<TEntity, bool>>cat, params Expression<Func<TEntity, object>>[] includes);
        //paging
        IQueryable<TEntity> GetAll(
        Expression<Func<TEntity, bool>>? filter = null,
        int? skip = null,
        int? take = null,
        params Expression<Func<TEntity, object>>[] includes);

        //Ge BY ID
        TEntity GetById(int id);
        //ADD
         void Add(TEntity entity);
        //UPDATE
          void Update(TEntity entity);
        //DELETE
          void Delete(int id);
        //Save
          void Save();
        //cart
        Cart GetCart(string userId);
        ICollection<Order> GetOrdersByUser(string userId);
    }
}
