using AutoMapper;
using HakunaMatata.API.Dto;
using HakunaMatata.Core.Models;

namespace HakunaMatata.API.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationGetDto>();

            CreateMap<ReservationCreateDto, Reservation>();

            CreateMap<ReservationUpdateDto, Reservation>();

            CreateMap<Property, ReservationPropertyDto>();

            CreateMap<Reservation, ReservationsDatesDto>();
        }
    }
}
