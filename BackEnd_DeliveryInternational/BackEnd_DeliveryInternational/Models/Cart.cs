namespace BackEnd_DeliveryInternational.Models
{
    public class Cart
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public int Amount { get; set; }
        public User user { get; set; }
        public Dish dish { get; set; }
        public Order? order { get; set; }
    }
}
