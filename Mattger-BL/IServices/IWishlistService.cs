using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_DAL.Entities;

namespace Mattger_BL.IServices
{
    public interface IWishlistService
    {
        IEnumerable<Wishlist> GetAll();

        Wishlist GetById(int id);

        Wishlist GetWishlist(string userId);

        void AddItem(string userId, int productId );

        void RemoveItem(string userId, int productId);

        void ClearWishlist(string userId);
        void MoveToCart(string userId);


    }
}
