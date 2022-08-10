using AutoMapper;
using HakunaMatata.API.Dto;
using HakunaMatata.Core.Models;

namespace HakunaMatata.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserGetDto>()
                .ForMember(u => u.UserId, opt => opt.MapFrom(s => s.UserId))
                .ForMember(u => u.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(u => u.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(u => u.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(u => u.Reservations, opt => opt.MapFrom(s => s.Reservations));

            CreateMap<UserCreateDto, User>()
                .ForMember(u => u.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(u => u.Password, opt => opt.MapFrom(s => s.Password))
                .ForMember(u => u.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(u => u.LastName, opt => opt.MapFrom(s => s.LastName));

            CreateMap<Property, UserPropertyDto>();
            CreateMap<Reservation, UserReservationDto>();
        }
    }
}
