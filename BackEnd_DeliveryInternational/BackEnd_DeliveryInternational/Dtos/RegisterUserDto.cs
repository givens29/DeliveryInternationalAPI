using BackEnd_DeliveryInternational.Models.Enums;

namespace BackEnd_DeliveryInternational.Dtos
{
    public class RegisterUserDto
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
