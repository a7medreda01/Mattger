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
    public class ProductReviewService : IProductReviewService
    {
        private readonly IGenericRepo<ProductReview> _repo;
        private readonly IGenericRepo<Product> _repoProduct;

        public ProductReviewService(IGenericRepo<ProductReview> repo, IGenericRepo<Product> repoProduct)
        {
            _repo = repo;
            _repoProduct = repoProduct;
        }

        public IEnumerable<ProductReview> GetAllByProduct(int productId)
        {
            return _repo.GetAll()
                .Where(r => r.ProductId == productId)
                .ToList();
        }

        public ProductReview GetById(int id)
        {
            return _repo.GetById(id);
        }

        public void Create(ProductReview review)
        {
            review.CreatedAt = DateTime.UtcNow;
            _repo.Add(review);
            _repo.Save();
            GetAverageRating(review.ProductId);

        }

        public void Update(ProductReview review)
        {
            _repo.Update(review);
            _repo.Save();
        }

        public void Delete(int id)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return;

            _repo.Delete(entity.Id);
            _repo.Save();
        }

        // ⭐ متوسط التقييم
        public double GetAverageRating(int productId)
        {
            var reviews = _repo.GetAll()
                .Where(r => r.ProductId == productId)
                .ToList();

            if (!reviews.Any())
                return 0;
            var product = _repoProduct.GetById(productId);
            var AverageRating= reviews.Average(r =>
                (r.Quality + r.Design + r.Usability + r.Durability + r.ValueForMoney) / 5.0
            );
            product.Rating = (decimal)AverageRating;
            _repoProduct.Save();

            return AverageRating;
        }
    }
}
