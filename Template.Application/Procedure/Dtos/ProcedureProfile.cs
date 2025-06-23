using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Procedure.Commands.Create;
using Template.Domain.Entities.ProcedureRelatedEntities;

namespace Template.Application.Procedure.Dtos;

public class ProcedureProfile:Profile
{
    public ProcedureProfile()
    {
        CreateMap<CreateProcedureCommand, Domain.Entities.ProcedureRelatedEntities.Procedure>().ReverseMap();
        CreateMap<ProcedureDto, Domain.Entities.ProcedureRelatedEntities.Procedure>().ReverseMap();

    }
}
