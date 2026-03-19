using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_DAL.Entities;

namespace Mattger_BL.IServices
{
    public interface ICartService
    {
        IEnumerable<Cart> GetAll();

        Cart GetById(int id);

        Cart GetCart(string userId);

        void AddItem(string userId, int productId, int quantity);

        void RemoveItem(string userId, int productId);

        void ClearCart(string userId);
    }
}
