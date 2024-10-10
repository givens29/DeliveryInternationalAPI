using BackEnd_DeliveryInternational.Models.Enums;
using System.Text.Json.Serialization;

namespace BackEnd_DeliveryInternational.Models
{
    public class User
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Order>? Orders { get; set; }
        public List<Rating>? Ratings { get; set; }
        public List<Dish>? Dishes { get; set; }
    }
}
