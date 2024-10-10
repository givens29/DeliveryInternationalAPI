using BackEnd_DeliveryInternational.Models;
using BackEnd_DeliveryInternational.Models.Enums;

namespace BackEnd_DeliveryInternational.Dtos
{
    public class ConcreteOrderDto
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public DateTime DeliveryTime { get; set; }
        public DateTime OrderTime { get; set; }
        public int Price { get; set; }
        public Status Status { get; set; }
    }
}
