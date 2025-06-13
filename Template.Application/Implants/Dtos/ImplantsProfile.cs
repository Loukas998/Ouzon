using AutoMapper;
using Template.Application.Implants.Commands.Create;
using Template.Application.Implants.Commands.Update;
using Template.Domain.Entities.Materials;

namespace Template.Application.Implants.Dtos;

public class ImplantsProfile : Profile
{
	public ImplantsProfile()
	{
		CreateMap<CreateImplantCommand, Implant>();
		CreateMap<UpdateImplantCommand, Implant>();	

		CreateMap<Implant, ImplantDto>();
	}
}
