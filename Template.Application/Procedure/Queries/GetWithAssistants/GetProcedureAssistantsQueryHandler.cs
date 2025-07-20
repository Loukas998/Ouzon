using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Queries;
using Template.Application.Procedure.Dtos.MainProcedure;
using Template.Application.Users.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Procedure.Queries.GetWithAssistants
{
    public class GetProcedureAssistantsQueryHandler(IProcedureRepository procedureRepository,IMapper mapper) : IQueryHandler<GetProcedureAssistantsQuery, List<UserDto>>
    {
        public async Task<Result<List<UserDto>>> Handle(GetProcedureAssistantsQuery request, CancellationToken cancellationToken)
        {
            var procedure = await procedureRepository.GetProcedureWithAssistants(request.Id);
            if (procedure == null)
            {
                return Result.Failure<List<UserDto>>(["Entity not Found"]);
            }
            var procDto = mapper.Map<ProcedureDto>(procedure);
            var assistList = mapper.Map<List<UserDto>>(procDto.Assistants);
            return Result.Success(assistList);
        }
    }
}
