using BackEnd_DeliveryInternational.Models.Enums;
using BackEnd_DeliveryInternational.Models;
using BackEnd_DeliveryInternational.Data;
using Microsoft.EntityFrameworkCore;
using BackEnd_DeliveryInternational.Dtos;

namespace BackEnd_DeliveryInternational.Services
{
    public interface IDishService
    {
        Task<Dish> AddDish(CreateDish dish);
        Task<ListDishesDto> GetAllDish(Category category, bool isVegetarian, Sorting sort, int page);
        Task<Dish> GetConcreteDish(Guid dishId);
        Task<bool> CheckUserCanRate(Guid dishId);
        Task<Dish> SetRating(Guid dishId, int rating);
    }
    public class DishService : IDishService
    {
        private readonly DataContext _context;

        public DishService(DataContext context)
        {
            _context = context;
        }

        public async Task<Dish> AddDish(CreateDish dish)
        {
            var Dish = new Dish
            {
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Photo = dish.Photo,
                IsVegetarian = dish.IsVegetarian,
                Rating = dish.Rating,
                Category = dish.Category
            };
            _context.Dishes.Add(Dish);

            await _context.SaveChangesAsync();
            return Dish;
        }

        public async Task<bool> CheckUserCanRate(Guid dishId)
        {
            var dish = _context.Dishes.FirstOrDefault(x => x.id == dishId);
            if (dish==null)
            {
                return false;
            }
            return true;
        }

        public async Task<ListDishesDto> GetAllDish(Category category, bool isVegetarian, Sorting sort, int page)
        {
            IQueryable<Dish> query = _context.Dishes.AsQueryable();

            if (category != null)
            {
                query = query.Where(d => d.Category == category);
            }

            if (isVegetarian)
            {
                query = query.Where(d => d.IsVegetarian);
            }

            switch (sort)
            {
                case Sorting.NameAsc:
                    query = query.OrderBy(d => d.Name);
                    break;
                case Sorting.NameDesc:
                    query = query.OrderByDescending(d => d.Name);
                    break;
                case Sorting.PriceAsc:
                    query = query.OrderBy(d => d.Price);
                    break;
                case Sorting.PriceDesc:
                    query = query.OrderByDescending(d => d.Price);
                    break;
                case Sorting.RatingAsc:
                    query = query.OrderBy(d => d.Rating);
                    break;
                case Sorting.RatingDesc:
                    query = query.OrderByDescending(d => d.Rating);
                    break;
                default:
                    query = query.OrderBy(d => d.Name);
                    break;
            }

            int pageSize = 5;

            int pageToQuery = Math.Max(page, 1);

            int skip = (pageToQuery - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);

            var result = new ListDishesDto
            {
                dishes = await query.ToListAsync(),
                pagination = new Pagination
                {
                    Size = pageSize,
                    Count = 1, 
                    Current = pageToQuery
                }
            };

            return result;
        }

        public async Task<Dish> GetConcreteDish(Guid dishId)
        {
            return await _context.Dishes.FindAsync(dishId);
        }

        public async Task<Dish> SetRating(Guid dishId, int rating)
        {
            Dish dish = await _context.Dishes.FindAsync(dishId);

            if (dish == null)
            {
                return null;
            }

            dish.Rating = rating;

            _context.Dishes.Update(dish);
            await _context.SaveChangesAsync();

            return dish;

        }
    }
}
