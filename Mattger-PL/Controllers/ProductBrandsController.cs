using AutoMapper;
using Mattger_BL.DTOs;
using Mattger_DAL.Entities;
using Mattger_DAL.IRepos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mattger_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBrandsController : ControllerBase
    {
        private readonly IGenericRepo<ProductBrand> _repo;
        private readonly IMapper _mapper;

        public ProductBrandsController(IGenericRepo<ProductBrand> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var brands = _repo.GetAll();
            return Ok(_mapper.Map<List<ProductBrandDTO>>(brands));
        }


    }
}
