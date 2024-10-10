using BackEnd_DeliveryInternational.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd_DeliveryInternational.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<IActionResult> GetCart()
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            if (userEmailClaim == null)
            {
                return Unauthorized();
            }
            var cart = await _basketService.GetCart(userEmailClaim.Value);
            if (cart == null)
            {
                return BadRequest();
            }
            return Ok(cart);

        }

        [HttpPost("dish/{dishId}")]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<IActionResult> AddDish(Guid dishId)
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            if (userEmailClaim == null)
            {
                return Unauthorized();
            }
            var addDish = await _basketService.AddDish(dishId, userEmailClaim.Value);
            return Ok(addDish);
        }

        [HttpDelete("dish/{dishId}")]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<IActionResult> DeleteDish(Guid dishId, bool increase)
        {
            var userEmailClaim = User.FindFirst(ClaimTypes.Email);
            if (userEmailClaim == null)
            {
                return Unauthorized();
            }
            var increaseDish = await _basketService.DeleteDish(dishId, increase);
            if (increaseDish == null)
            {
                return BadRequest();
            }
            return Ok(increaseDish);
        }
    }
}
