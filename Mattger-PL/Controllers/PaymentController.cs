using Mattger_BL.DTOs;
using Mattger_BL.IServices;
using Mattger_DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mattger_PL.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentMethodService _service;

        public PaymentController(IPaymentMethodService service)
        {
            _service = service;
        }

        // GET: api/paymentmethod/user/{userId}
        [HttpGet("user/{userId}")]
        public IActionResult GetAll(string userId)
        {
            var result = _service.GetAll(userId);
            return Ok(result);
        }

        // GET: api/paymentmethod/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/paymentmethod
        [HttpPost]
        public IActionResult Create(PaymentMethodDDTO dto)
        {
            var entity = new PaymentMethod
            {
                UserId = dto.UserId,
                CardType = dto.CardType,
                CardName = dto.CardName,
                CardNumber = dto.CardNumber,
                ExpiryDate = dto.ExpiryDate,
            };

            _service.Create(entity);
            return Ok("Payment Method Created Successfully");
        }

        // DELETE: api/paymentmethod/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok("Deleted Successfully");
        }
    }
}
