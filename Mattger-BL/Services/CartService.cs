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
    public class CartService :ICartService
    {
        private readonly IGenericRepo<Cart> _repo;
        private readonly IGenericRepo<CartItem> _cartItemRepo;
        private readonly IGenericRepo<Product> _productRepo;

        public CartService(
            IGenericRepo<Cart> cartRepo,
            IGenericRepo<CartItem> cartItemRepo,
            IGenericRepo<Product> productRepo)
        {
            _repo = cartRepo;
            _cartItemRepo = cartItemRepo;
            _productRepo = productRepo;
        }

        public IEnumerable<Cart> GetAll()
        {
            return _repo.GetAll();
        }

        public Cart GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Add(Cart cart)
        {
            _repo.Add(cart);
            _repo.Save();
        }

        public void Update(Cart cart)
        {
            _repo.Update(cart);
            _repo.Save();
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }

        public Cart GetCart(string userId)
        {
            var cart = _repo
                .GetCart(userId);

            return cart;
        }

        public void AddItem(string userId, int productId, int quantity)
        {
            var cart = _repo
                .GetAll(c => c.Items)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>()
                };

                _repo.Add(cart);
                _repo.Save();
            }

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                item.Quantity = quantity;
                _cartItemRepo.Update(item);
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity
                };

                _cartItemRepo.Add(cartItem);
            }

            _cartItemRepo.Save();
        }


        public void RemoveItem(string userId, int productId)
        {
            var cart = _repo
                .GetAll(c => c.Items)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
                return;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                _cartItemRepo.Delete(item.Id);
                _cartItemRepo.Save();
            }
        }

        public void ClearCart(string userId)
        {
            var cart = _repo
               .GetAll(c => c.Items)
               .FirstOrDefault(c => c.UserId == userId);
            _repo.Delete(cart.Id);
            _repo.Save();
        }
    }
}
