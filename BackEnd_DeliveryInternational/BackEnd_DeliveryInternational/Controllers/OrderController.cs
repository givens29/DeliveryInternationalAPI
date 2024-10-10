using BackEnd_DeliveryInternational.Dtos;
using BackEnd_DeliveryInternational.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd_DeliveryInternational.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<IActionResult> GetConcreteOrder(Guid id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Unauthorized();
            }
            var userOrder = await _orderService.GetConcreteOrder(id);
            if (userOrder == null)
            {
                return BadRequest();
            }
            return Ok(userOrder);
        }

        [HttpGet]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<IActionResult> GetListOfOrders()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Unauthorized();
            }
            var userOrder = await _orderService.GetListOrders(userEmail.Value);
            if(userOrder== null)
            {
                return BadRequest();
            }
            return Ok(userOrder);
        }

        [HttpPost]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<IActionResult> CreateOrder(CreateOrderDto model)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Unauthorized();
            }
            var order = await _orderService.CreateOrder(userEmail.Value, model);
            return Ok(order);
        }

        [HttpPost("{id}/status")]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<IActionResult> ConfirmDelivery(Guid id)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Unauthorized();
            }
            var order = await _orderService.ConfirmDelivery(id);
            if (!order)
            {
                return BadRequest();
            }
            return Ok(order);
        }
    }
}
