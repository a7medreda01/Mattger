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
    public class WishlistService : IWishlistService
    {
        private readonly IGenericRepo<Wishlist> _repo;
        private readonly IGenericRepo<Cart> _repoCart;
        private readonly IGenericRepo<WishlistItem> _WishlistItemRepo;
        private readonly IGenericRepo<CartItem> _CartItemRepo;
        private readonly IGenericRepo<Product> _productRepo;

        public WishlistService(
            IGenericRepo<Wishlist> WishlistRepo,
            IGenericRepo<WishlistItem> WishlistItemRepo,
            IGenericRepo<CartItem> CartItemRepo,
            IGenericRepo<Product> productRepo,
            IGenericRepo<Cart> repoCart)
        {
            _repo = WishlistRepo;
            _WishlistItemRepo = WishlistItemRepo;
            _CartItemRepo = CartItemRepo;
            _productRepo = productRepo;
            _repoCart = repoCart;
        }

        public IEnumerable<Wishlist> GetAll()
        {
            return _repo.GetAll();
        }

        public Wishlist GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Add(Wishlist Wishlist)
        {
            _repo.Add(Wishlist);
            _repo.Save();
        }

        public void Update(Wishlist Wishlist)
        {
            _repo.Update(Wishlist);
            _repo.Save();
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }

        public Wishlist GetWishlist(string userId)
        {
            var Wishlist = _repo
                .GetWishlist(userId);

            return Wishlist;
        }

        public void AddItem(string userId, int productId)
        {
            var Wishlist = _repo
                .GetAll(c => c.Items)
                .FirstOrDefault(c => c.UserId == userId);

            if (Wishlist == null)
            {
                Wishlist = new Wishlist
                {
                    UserId = userId,
                    Items = new List<WishlistItem>()
                };

                _repo.Add(Wishlist);
                _repo.Save();
            }

            var item = Wishlist.Items.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                
                _WishlistItemRepo.Update(item);
            }
            else
            {
                var WishlisttItem = new WishlistItem
                {
                    WishlistId = Wishlist.Id,
                    ProductId = productId,
                    
                };

                _WishlistItemRepo.Add(WishlisttItem);
            }

            _WishlistItemRepo.Save();
        }


        public void RemoveItem(string userId, int productId)
        {
            var Wishlist = _repo
                .GetAll(c => c.Items)
                .FirstOrDefault(c => c.UserId == userId);

            if (Wishlist == null)
                return;

            var item = Wishlist.Items.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                _WishlistItemRepo.Delete(item.Id);
                _WishlistItemRepo.Save();
            }
        }

        public void ClearWishlist(string userId)
        {
            // جيب الكارت مع العناصر
            var Wishlist = _repo
                .GetAll(c => c.Items)
                .FirstOrDefault(c => c.UserId == userId);

            if (Wishlist != null && Wishlist.Items.Any())
            {
                // مسح كل العناصر من الكارت
                Wishlist.Items.Clear();
                _repo.Save(); // حفظ التغييرات
            }
        }
        public void MoveToCart(string userId)
        {
            var wishlist = _repo
                .GetAll(c => c.Items)
                .FirstOrDefault(c => c.UserId == userId);

            if (wishlist == null || !wishlist.Items.Any())
                return;

            // جلب أو إنشاء Cart للمستخدم
            var cart = _repoCart
                .GetAll(c => c.Items)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>()
                };

                _repoCart.Add(cart);
            }

            foreach (var item in wishlist.Items)
            {
                // منع التكرار
                var exists = cart.Items
                    .Any(ci => ci.ProductId == item.ProductId);

                if (exists)
                    continue;

                cart.Items.Add(new CartItem
                {
                    ProductId = item.ProductId,
                    Quantity = 1
                });
            }

            _repoCart.Save();
        }
    }
}
