namespace BackEnd_DeliveryInternational.Dtos
{
    public class DishOrderDto
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }
        public int Amount { get; set; }
        public string Image { get; set; }
    }
}
