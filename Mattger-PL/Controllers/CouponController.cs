using AutoMapper;
using Mattger_BL.DTOs;
using Mattger_BL.IServices;
using Mattger_DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Mattger_PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _service;
        private readonly IMapper _mapper;

        public CouponController(ICouponService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/coupon
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        // GET: api/coupon/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var coupon = _service.GetById(id);

            if (coupon == null)
                return NotFound();

            return Ok(coupon);
        }

        // GET: api/coupon/code/SAVE10
        [HttpGet("code/{code}")]
        public IActionResult GetByCode(string code)
        {
            var coupon = _service.GetByCode(code);

            if (coupon == null)
                return NotFound();

            return Ok(coupon);
        }

        // POST: api/coupon
        [HttpPost]
        public IActionResult Create(CouponDTO coupon)
        {
           var cop = _mapper.Map<Coupon>(coupon);
            _service.Create(cop);
            return Ok("Coupon created successfully");
        }

        // PUT: api/coupon
        [HttpPut]
        public IActionResult Update(Coupon coupon)
        {
            _service.Update(coupon);
            return Ok("Coupon updated successfully");
        }

        // DELETE: api/coupon/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok("Coupon deleted successfully");
        }

        // GET: api/coupon/validate/SAVE10
        [HttpGet("validate/{code}")]
        public IActionResult Validate(string code)
        {
            var isValid = _service.IsValid(code);
            return Ok(new { IsValid = isValid });
        }
        [HttpPost("apply")]
        public IActionResult ApplyCoupon([FromBody] ApplyCouponDto dto)
        {
            var result = _service.ApplyCoupon(dto);

            if (!result.IsValid)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
