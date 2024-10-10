using BackEnd_DeliveryInternational.Models;

namespace BackEnd_DeliveryInternational.Dtos
{
    public class ListDishesDto
    {
        public List<Dish> dishes { get; set; }
        public Pagination pagination { get; set; }

    }
}
