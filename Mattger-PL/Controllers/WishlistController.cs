using AutoMapper;
using Mattger_BL.DTOs;
using Mattger_BL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mattger_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _service;
        private readonly IMapper _mapper;

        public WishlistController(IWishlistService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public IActionResult GetWishlist(string userId)
        {
            var Wishlist = _service.GetWishlist(userId);
            var WishlistDto = _mapper.Map<WishlistDTO>(Wishlist);
            return Ok(WishlistDto);
        }

        [HttpPost("update")]
        public IActionResult AddItem(AddToWishlistDTO dto)
        {
            _service.AddItem(dto.UserId, dto.ProductId );

            return Ok();
        }

        [HttpPut("removeItem")]
        public IActionResult RemoveItem(AddToWishlistDTO dto)
        {
            _service.RemoveItem(dto.UserId, dto.ProductId);
            return Ok();
        }

        [HttpDelete("clearWishlist")]
        public IActionResult ClearCart(string Uid)
        {
            _service.ClearWishlist(Uid);
            return Ok();
        }
        [HttpPost("moveCart")]
        public IActionResult MoveToCart(string Uid)
        {
            _service.MoveToCart(Uid);
            return Ok();
        }
    }
}
