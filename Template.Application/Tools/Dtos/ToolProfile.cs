using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Tools.Commands.Create;
using Template.Application.Tools.Commands.Update;
using Template.Domain.Entities.Materials;

namespace Template.Application.Tools.Dtos;

public class ToolProfile :Profile
{
    public ToolProfile()
    {
        CreateMap<ToolDto, Tool>().ReverseMap();
        CreateMap<CreateToolCommand, Tool>().ReverseMap();
        CreateMap<UpdateToolCommand, Tool>().ReverseMap();
    }
}
