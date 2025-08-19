using AutoMapper;
using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;

namespace Ecommerce_Backend.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Product mappings
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));

            // Order mappings
            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));  // <-- user mapping

            // OrderItem mappings
            CreateMap<OrderItem, OrderItemResponseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            // Address mappings
            CreateMap<Address, AddressDTO>();

            // User mappings
            CreateMap<User, UserDto>();
        }
    }
}
