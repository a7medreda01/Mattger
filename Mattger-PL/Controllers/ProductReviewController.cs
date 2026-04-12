using AutoMapper;
using Mattger_BL.IServices;
using Mattger_DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mattger_PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductReviewController : ControllerBase
    {
        private readonly IProductReviewService _service;
        private readonly IMapper mapper;

        public ProductReviewController(IProductReviewService service, IMapper mapper)
        {
            _service = service;
            this.mapper = mapper;
        }

        // GET: api/productreview/product/5
        [HttpGet("product/{productId}")]
        public IActionResult GetByProduct(int productId)
        {
            var result = _service.GetAllByProduct(productId);
            return Ok(result);
        }

        // GET: api/productreview/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/productreview
        [HttpPost]
        public IActionResult Create(ProductReviewDTO review)
        {
            var rev = mapper.Map<ProductReview>(review);
            _service.Create(rev);
            return Ok("Review created successfully");
        }

        // PUT: api/productreview
        [HttpPut]
        public IActionResult Update(ProductReview review)
        {
            _service.Update(review);
            return Ok("Review updated successfully");
        }

        // DELETE: api/productreview/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok("Review deleted successfully");
        }

        // GET: api/productreview/average/5
        [HttpGet("average/{productId}")]
        public IActionResult GetAverage(int productId)
        {
            var avg = _service.GetAverageRating(productId);
            return Ok(new { ProductId = productId, AverageRating = avg });
        }
    }
}