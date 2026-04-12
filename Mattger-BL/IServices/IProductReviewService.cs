using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mattger_DAL.Entities;

namespace Mattger_BL.IServices
{
    public interface IProductReviewService
    {
        IEnumerable<ProductReview> GetAllByProduct(int productId);
        ProductReview GetById(int id);
        void Create(ProductReview review);
        void Update(ProductReview review);
        void Delete(int id);

        double GetAverageRating(int productId);
    }
}
