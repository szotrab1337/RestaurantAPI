using AutoMapper;
using RestaurantAPI.Entites;
using RestaurantAPI.Models;

namespace RestaurantAPI
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(x => x.City, y => y.MapFrom(z => z.Address.City))
                .ForMember(x => x.Street, y => y.MapFrom(z => z.Address.Street))
                .ForMember(x => x.PostalCode, y => y.MapFrom(z => z.Address.PostalCode));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(x => x.Address,
                    y => y.MapFrom(dto => new Address
                    {
                        City = dto.City,
                        PostalCode = dto.PostalCode,
                        Street = dto.Street
                    }));
        }
    }
}
