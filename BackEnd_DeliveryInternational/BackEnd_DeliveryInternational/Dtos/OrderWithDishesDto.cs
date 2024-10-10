using BackEnd_DeliveryInternational.Models.Enums;
using BackEnd_DeliveryInternational.Models;

namespace BackEnd_DeliveryInternational.Dtos
{
    public class OrderWithDishesDto
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public DateTime DeliveryTime { get; set; }
        public DateTime OrderTime { get; set; }
        public int Price { get; set; }
        public string? Address { get; set; }
        public Status Status { get; set; }
        public List<DishOrderDto> Dishes { get; set; }
    }
}
