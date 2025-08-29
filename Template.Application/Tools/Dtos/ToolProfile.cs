using AutoMapper;
using Template.Application.Tools.Commands.Create;
using Template.Application.Tools.Commands.Update;
using Template.Domain.Entities.Materials;

namespace Template.Application.Tools.Dtos;

public class ToolProfile : Profile
{
    public ToolProfile()
    {
        CreateMap<Tool, ToolDto>()
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom<ToolImageUrlResolver>())
            .ReverseMap();

        CreateMap<CreateToolCommand, Tool>().ReverseMap();
        CreateMap<UpdateToolCommand, Tool>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
    }
}
