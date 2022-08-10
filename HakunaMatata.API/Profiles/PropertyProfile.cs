using AutoMapper;
using HakunaMatata.API.Dto;
using HakunaMatata.Core.Models;

namespace HakunaMatata.API.Profiles
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Property, PropertyGetDto>()
                .ForMember(p => p.PropertyId, opt => opt.MapFrom(s => s.PropertyId))
                .ForMember(p => p.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(p => p.MaxGuests, opt => opt.MapFrom(s => s.MaxGuests))
                .ForMember(p => p.Address, opt => opt.MapFrom(s => s.Address))
                .ForMember(p => p.Price, opt => opt.MapFrom(s => s.Price))
                .ForMember(p => p.Images, opt => opt.MapFrom(s => s.Images));

            CreateMap<PropertyCreateDto, Property>()
                .ForMember(p => p.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(p => p.MaxGuests, opt => opt.MapFrom(s => s.MaxGuests))
                .ForMember(p => p.Address, opt => opt.MapFrom(s => s.Address))
                .ForMember(p => p.Price, opt => opt.MapFrom(s => s.Price));
        }
    }
}
