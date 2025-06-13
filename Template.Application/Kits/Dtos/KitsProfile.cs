using AutoMapper;
using Template.Application.Kits.Commands.Create;
using Template.Domain.Entities.Materials;

namespace Template.Application.Kits.Dtos;

public class KitsProfile : Profile
{
	public KitsProfile()
	{
		CreateMap<Kit, KitDto>();

		CreateMap<CreateKitCommand, Kit>();
	}
}
