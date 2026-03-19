using AutoMapper;
using Mattger_BL.DTOs;
using Mattger_BL.IServices;
using Mattger_DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mattger_PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;
        private readonly IMapper _mapper;

        public CartController(ICartService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public IActionResult GetCart(string userId)
        {
            var cart = _service.GetCart(userId); 
            var cartDto = _mapper.Map<CartDTO>(cart);
            return Ok(cartDto);
        }

        [HttpPost("update")]
        public IActionResult AddItem(AddCartItemDTO dto)
        {
            _service.AddItem(dto.UserId,dto.ProductId, dto.Quantity);

            return Ok();
        }
        
        [HttpPut("removeItem")]
        public IActionResult RemoveItem(AddCartItemDTO dto)
        {
            _service.RemoveItem(dto.UserId, dto.ProductId);
            return Ok();
        }

        [HttpDelete("clearCart")]
        public IActionResult ClearCart(string Uid)
        {
            _service.ClearCart(Uid);
            return Ok();
        }
    }
}
