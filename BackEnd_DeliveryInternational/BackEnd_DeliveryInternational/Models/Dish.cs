using BackEnd_DeliveryInternational.Models.Enums;

namespace BackEnd_DeliveryInternational.Models
{
    public class Dish
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Photo { get; set; }
        public bool IsVegetarian { get; set; }
        public int Rating { get; set; }
        public Category Category { get; set; }
        public List<Rating>? Ratings { get; set; }
    }
}
