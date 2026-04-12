using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAllAsync([FromQuery] ProductQueryParams param)
        {
            var products = await  service.GetAllProductsAsync(param);
            var result = mapper.Map<ICollection<ProductDTO>>(products);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product =await service.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            var result = mapper.Map<ProductDTO>(product);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(CreateProductDTO productDto)
        {
            //var result = mapper.Map<Product>(product);
            service.Add(productDto);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] UpdateProductDTO productDTO)
        {
            service.Update(id, productDTO);
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
