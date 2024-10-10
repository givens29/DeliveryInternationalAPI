using BackEnd_DeliveryInternational.Models.Enums;

namespace BackEnd_DeliveryInternational.Dtos
{
    public class CreateDish
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Photo { get; set; }
        public bool IsVegetarian { get; set; }
        public int Rating { get; set; }
        public Category Category { get; set; }
    }
}
