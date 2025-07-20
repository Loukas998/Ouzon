using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Implants.Dtos;
using Template.Application.Kits.Dtos;
using Template.Application.Procedure.Commands.Create;
using Template.Application.Procedure.Commands.Update;
using Template.Application.Tools.Dtos;
using Template.Domain.Entities.Materials;
using Template.Domain.Entities.ProcedureRelatedEntities;

namespace Template.Application.Procedure.Dtos;

public class ProcedureProfile:Profile
{
    public ProcedureProfile()
    {
        CreateMap<CreateProcedureCommand, Domain.Entities.ProcedureRelatedEntities.Procedure>();

        CreateMap<Domain.Entities.ProcedureRelatedEntities.Procedure, ProcedureDto>()
            .ForMember(dest => dest.Tools, opt => opt.MapFrom(src => src.ToolsInProcedure.Select(tp => tp.Tool)))
            .ForMember(dest => dest.Kits, opt => opt.MapFrom(src => src.KitsInProcedure.Select(kp => kp.Kit)))
            .ForMember(dest => dest.Assistants, opt => opt.MapFrom(src => src.AssistantsInProcedure.Select(kp => kp.Asisstant)))
            .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor))
            .ReverseMap();

        CreateMap<UpdateProcedureCommand, Domain.Entities.ProcedureRelatedEntities.Procedure>().ReverseMap();

        CreateMap<ProcedureSummaryDto, Domain.Entities.ProcedureRelatedEntities.Procedure>().ReverseMap();

        CreateMap<ProcedureKitDetailsDto, ProcedureDto>().ReverseMap();

        CreateMap<Kit,ProcedureImplantToolsDetailsDto>()
            .ForMember(dest => dest.Implant, opt => opt.MapFrom(src => src.Implants.FirstOrDefault()))
            .ForMember(dest => dest.ToolsWithImplant, opt => opt.MapFrom(src => src.Tools));

        CreateMap<KitDto, ProcedureImplantToolsDetailsDto>()
    .ForMember(dest => dest.Implant, opt => opt.MapFrom(src => src.Implants.FirstOrDefault()))
    .ForMember(dest => dest.ToolsWithImplant, opt => opt.MapFrom(src => src.Tools))
    .ReverseMap()
    ;
        CreateMap<ImplantDto, ProcedureImplantToolsDetailsDto>().ReverseMap();
    }
}
