using BackEnd_DeliveryInternational.Dtos;
using BackEnd_DeliveryInternational.Models.Enums;
using BackEnd_DeliveryInternational.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd_DeliveryInternational.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListDishes(Category category, bool isVegetarian, Sorting sort, int page)
        {
            var dishes = await _dishService.GetAllDish(category, isVegetarian, sort, page);
            if (dishes == null)
            {
                BadRequest();
            }
            return Ok(dishes);

        }

        [HttpGet("{dishId}")]
        public async Task<IActionResult> GetConcreteDish(Guid dishId)
        {
            var dish = await _dishService.GetConcreteDish(dishId);
            return Ok(dish);

        }

        [HttpGet("{dishId}/rating/check")]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<IActionResult> CheckRating(Guid dishId)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);
            if (userEmail == null)
            {
                return Unauthorized();
            }
            var dish = await _dishService.CheckUserCanRate(dishId);
            if (!dish)
            {
                return BadRequest();
            }
            return Ok(dish);

        }

        //[HttpPost]
        //public async Task<IActionResult> AddDish(CreateDish dish)
        //{
        //    var addDish = await _dishService.AddDish(dish);
        //    return Ok(addDish);
        //}

        [HttpPost("{id}/rating")]
        [Authorize(Policy = "AnyAuthenticatedUser")]
        public async Task<IActionResult> SetRating(Guid id, int rating)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email);
            if(userEmail == null)
            {
                return Unauthorized();
            }
            var setRating = await _dishService.SetRating(id, rating);
            if (setRating == null)
            {
                return BadRequest();
            }
            return Ok(setRating);

        }
    }
}
