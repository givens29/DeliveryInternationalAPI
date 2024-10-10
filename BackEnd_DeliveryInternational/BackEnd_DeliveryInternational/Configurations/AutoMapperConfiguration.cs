using AutoMapper;
using BackEnd_DeliveryInternational.Dtos;
using BackEnd_DeliveryInternational.Models;
namespace BackEnd_DeliveryInternational.Configurations
{
    public class AutoMapperConfiguration:Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Cart, DishOrderDto>()
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.dish != null ? src.dish.Name : ""))
    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.dish != null ? src.dish.Price : 0))
    .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.dish != null ? src.dish.Price + src.Amount : 0))
    .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.dish != null ? src.dish.Photo : ""));

        }

    }
}
