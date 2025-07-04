using AutoMapper;
using Template.Application.Kits.Commands.Create;
using Template.Domain.Entities.Materials;

namespace Template.Application.Kits.Dtos;

public class KitsProfile : Profile
{
	public KitsProfile()
	{
		CreateMap<Kit, KitDto>()
			.ForMember(dest => dest.Implants, opt => opt.MapFrom(src => src.Implants)).ReverseMap();



        CreateMap<CreateKitCommand, Kit>();
	}
}
