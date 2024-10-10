namespace BackEnd_DeliveryInternational.Models
{
    public class StorageUsersTokens
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string email { get; set; }
        public string Token { get; set; }
    }
}
