using BackEnd_DeliveryInternational.Models.Enums;

namespace BackEnd_DeliveryInternational.Dtos
{
    public class ProfileUserDto
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
