using AutoMapper;
using Mattger_BL.DTOs;
using Mattger_BL.DTOs.Mattger_BL.DTOs;
using Mattger_BL.IServices;
using Mattger_DAL.Entities;
using Mattger_DAL.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mattger_PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        //[Authorize]

        public IActionResult GetAllOrders()
        {
            var orders = _service.GetAllOrders();
            return Ok(_mapper.Map<List<OrderDTO>>(orders));
        }
        [HttpGet("user/{userId}")]
        public IActionResult GetOrders(string userId)
        {
            var orders = _service.GetOrders(userId);
            List< OrderDTO > ordersUser = _mapper.Map<List<OrderDTO>>(orders);
            return Ok(ordersUser);
        }
        
        [HttpGet("{orderId}")]
        public IActionResult GetOrderById(int orderId)
        {
            var order = _service.GetOrderById(orderId);
            var orderDto = _mapper.Map<OrderDTO>(order);
            return Ok(orderDto);
        }
        [HttpPut("update")]
        public IActionResult UpdateStatus(int orderId, OrderStatus newStatus)
        {
             _service.UpdateOrder(orderId,newStatus);
            var order = _service.GetOrderById(orderId);
            return Ok(_mapper.Map<OrderDTO>(order));
        }

        [HttpPost("create")]
        public IActionResult CreateOrder(CreateOrderDTO dto)
        {
            var order = _mapper.Map<Order>(dto);
            _service.CreateOrder(dto.UserId,order);

            return Ok();
        }
        [HttpPut("cancel/{orderId}")]
        public IActionResult CancelOrder(int orderId)
        {
            _service.CancelOrder(orderId);
            var order = _service.GetOrderById(orderId);
            return Ok(_mapper.Map<OrderDTO>(order));
        }

    }
}
