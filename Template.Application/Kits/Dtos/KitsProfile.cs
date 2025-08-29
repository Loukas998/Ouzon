using AutoMapper;
using Template.Application.Kits.Commands.Create;
using Template.Application.Kits.Commands.Update;
using Template.Domain.Entities.Materials;

namespace Template.Application.Kits.Dtos;

public class KitsProfile : Profile
{
    public KitsProfile()
    {
        CreateMap<Kit, KitDto>()
            .ForMember(dest => dest.Implants, opt => opt.MapFrom(src => src.Implants))
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom<KitImageUrlResolver>())
            .ReverseMap();

        CreateMap<UpdateKitCommand, Kit>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());

        CreateMap<CreateKitCommand, Kit>();
    }
}
