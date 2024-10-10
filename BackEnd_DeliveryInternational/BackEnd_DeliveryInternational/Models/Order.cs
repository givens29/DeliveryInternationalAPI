using BackEnd_DeliveryInternational.Models.Enums;
using System.Text.Json.Serialization;

namespace BackEnd_DeliveryInternational.Models
{
    public class Order
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public DateTime DeliveryTime { get; set; }
        public DateTime OrderTime { get; set; }
        public int Price { get; set; }
        public string? Address { get; set; }
        public Status Status { get; set; }
        public List<Cart> Dishes { get; set; }
    }
}
