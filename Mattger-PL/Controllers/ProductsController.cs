using AutoMapper;
using Mattger_BL.DTOs;
using Mattger_BL.IServices;
using Mattger_DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mattger_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService service;
        private readonly IMapper mapper;

        public ProductsController(IProductService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = service.GetAll();
            var result = mapper.Map<ICollection<ProductDTO>>(products);
            return Ok(result);
        }
        [HttpGet("search")]
        public IActionResult GetAll([FromQuery]string search)
        {
            var products = service.GetAll(search);
            var result = mapper.Map<ICollection<ProductDTO>>(products);
            return Ok(result);
        }
        [HttpGet("category")]
        public IActionResult GetAll([FromQuery] int? category=null)
        {
            var products = service.GetAll(category);
            var result = mapper.Map<ICollection<ProductDTO>>(products);
            return Ok(result);
        }
        //paging
        [HttpGet("pagination")]
        public IActionResult GetProducts(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] int? category = null,
        [FromQuery] string? search = null)

        {
            var (items, totalItems) = service.GetProductsAsync(page, pageSize, category, search).Result;
            var products = mapper.Map<ICollection<ProductDTO>>(items);
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            return Ok(new
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = products
            });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = service.GetById(id);
            if (product == null)
                return NotFound();
            var result = mapper.Map<ProductDTO>(product);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(CreateProductDTO product)
        {
            var result = mapper.Map<Product>(product);
            service.Add(result);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, CreateProductDTO productDTO)
        {
            var product = mapper.Map<Product>(productDTO);
            product.Id = id;
            service.Update(product);
            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return Ok();
        }
    }
}
