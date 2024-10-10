using BackEnd_DeliveryInternational.Data;
using BackEnd_DeliveryInternational.Dtos;
using BackEnd_DeliveryInternational.Models;
using BackEnd_DeliveryInternational.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using AutoMapper;
using BackEnd_DeliveryInternational.Configurations;

namespace BackEnd_DeliveryInternational.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(string userEmail, CreateOrderDto orderDto);
        Task<List<ConcreteOrderDto>> GetListOrders(string email);
        Task<OrderWithDishesDto> GetConcreteOrder(Guid id);
        Task<bool> ConfirmDelivery(Guid id);
    }
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OrderService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ConfirmDelivery(Guid id)
        {
            var userOrder = await _context.Orders.FirstOrDefaultAsync(x => x.id == id);

            if (userOrder != null)
            {
                userOrder.Status = Status.Delivered;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Order> CreateOrder(string userEmail, CreateOrderDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return null;
            }
            if (user.Orders == null)
            {
                user.Orders = new List<Order>();
            }
            var userDishes = await _context.Carts
                .Include(cart => cart.order)
                .Include(cart => cart.dish)
                .Where(cart => cart.user.id == user.id)
                .ToListAsync();
            if (userDishes.Any(x => x.order != null))
            {
                throw new Exception("Order already created");
            }

            int numberOfDishesInCart = userDishes.Count;
            var totalOrderPrice = userDishes.Sum(cart => cart.dish.Price * cart.Amount);
            
            var userOrder = new Order
            {
                DeliveryTime = model.DeliveryTime,
                OrderTime = DateTime.Now,
                Status = Status.InProcess,
                Address = model.Address,
                Price = totalOrderPrice,
                Dishes = userDishes.ToList()
            };

            foreach (var cartItem in userDishes)
            {
                cartItem.order = userOrder;
            }

            user.Orders.Add(userOrder);

            _context.Orders.Add(userOrder);
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            var jsonString = JsonSerializer.Serialize(userOrder, jsonSerializerOptions);


            await _context.SaveChangesAsync();

            return userOrder;
        }

        public async Task<OrderWithDishesDto> GetConcreteOrder(Guid id)
        {
            var order = await _context.Orders
        .Include(o => o.Dishes)
        .ThenInclude(x=> x.dish)
        .FirstOrDefaultAsync(x => x.id == id);

            if (order == null)
            {
                return null;
            }

           

            var userOrder = new OrderWithDishesDto
            {
                id = order.id,
                DeliveryTime = order.DeliveryTime,
                OrderTime = order.OrderTime,
                Status = order.Status,
                Address = order.Address,
                Price = order.Price,
                Dishes = _mapper.Map<List<DishOrderDto>>(order.Dishes)
            };

            return userOrder;

        }

        public async Task<List<ConcreteOrderDto>> GetListOrders(string email)
        {
            var user = await _context.Users
        .Include(u => u.Orders)
        .ThenInclude(o => o.Dishes)
        .FirstOrDefaultAsync(x => x.Email == email);

            if (user.Orders == null || !user.Orders.Any())
            {
                return null;
            }
            var orderList = user.Orders.Select(order => new ConcreteOrderDto
            {
                id = order.id,
                DeliveryTime = order.DeliveryTime,
                OrderTime = order.OrderTime,
                Status = order.Status,
                Price= order.Price

            }).ToList();

            return orderList;
        }
    }
}
