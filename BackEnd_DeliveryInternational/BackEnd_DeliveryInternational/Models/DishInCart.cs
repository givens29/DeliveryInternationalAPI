namespace BackEnd_DeliveryInternational.Models
{
    public class DishInCart
    {
        Guid id { get; set; } = new Guid();
        public int Count { get; set; }
        public Order? Order { get; set; }
    }
}
