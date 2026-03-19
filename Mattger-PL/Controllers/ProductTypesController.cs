using AutoMapper;
using Mattger_BL.DTOs;
using Mattger_BL.IServices;
using Mattger_DAL.Entities;
using Mattger_DAL.IRepos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mattger_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypesController : ControllerBase
    {
        private readonly ITypeService _service;
        private readonly IMapper _mapper;

        public ProductTypesController(ITypeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var types = _service.GetAll();
            return Ok(_mapper.Map<List<ProductTypeDTO>>(types));
        }
        [HttpPut("Create")]
        public IActionResult Create(CreateProductTypeDTO type)
        {
            var newType = _mapper.Map<ProductType>(type);
            _service.Add(newType);
            return Ok(newType);
        }
        [HttpPut("Update")]
        public IActionResult Update(int Tid, CreateProductTypeDTO newType)
        {
            var type = _mapper.Map<ProductType>(newType);
            _service.Update(Tid, type);
            return Ok(newType);
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}
