using BackEnd_DeliveryInternational.Data;
using Microsoft.EntityFrameworkCore;
using BackEnd_DeliveryInternational.Models;
using BackEnd_DeliveryInternational.Dtos;

namespace BackEnd_DeliveryInternational.Services
{
    public interface IBasketService
    {
        Task<string> AddDish(Guid dishId, string email);
        Task<string> DeleteDish(Guid dishId, bool increase);
        Task<DishOrderDto> GetCart(string email);
    }
    public class BasketService : IBasketService
    {
        private readonly DataContext _context;

        public BasketService(DataContext context)
        {
            _context = context;
        }

        public async Task<string> AddDish(Guid dishId, string email)
        {
            var User = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            
            var Dish = await _context.Dishes.FindAsync(dishId);
            if (Dish == null)
            {
                return null;
            }

            var Cart = await _context.Carts.FirstOrDefaultAsync(u => u.dish.id == dishId);
            if (Cart != null)
            {
                Cart.Amount = Cart.Amount + 1;
                await _context.SaveChangesAsync();
                return "Dish added!";
            }

            var cart = new Cart
            {
                Amount = 1,
                user = User,
                dish = Dish      
            };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return "Dish added!";

        }

        public async Task<string> DeleteDish(Guid dishId, bool increase)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(u => u.dish.id == dishId);
            if (cart != null)
            {
                if (increase == true)
                {
                    cart.Amount = cart.Amount + 1;
                    await _context.SaveChangesAsync();
                    return "Dish inrease";
                }
                else
                {
                    _context.Carts.Remove(cart);
                    await _context.SaveChangesAsync();
                    return "Dish remove";
                }
            }
            return null;
        }

        public async Task<DishOrderDto> GetCart(string email)
        {
            var User = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            var userCart = await _context.Carts.Include(c => c.dish).FirstOrDefaultAsync(u => u.user == User);
            if (userCart != null)
            {
                if (userCart.dish == null)
                {
                    return null;
                }
                var displayCart = new DishOrderDto
                {
                    id = userCart.id,
                    Name = userCart.dish.Name,
                    Price = userCart.dish.Price,
                    TotalPrice = userCart.dish.Price * userCart.Amount,
                    Amount = userCart.Amount,
                    Image = userCart.dish.Photo
                };
                return displayCart;

            }
            return null;
        }
    }
}
