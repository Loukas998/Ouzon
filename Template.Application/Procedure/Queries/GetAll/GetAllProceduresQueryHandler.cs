using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Queries;
using Template.Application.Procedure.Dtos.MainProcedure;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Procedure.Queries.GetAll
{
    public class GetAllProceduresQueryHandler(IProcedureRepository procedureRepository,
        IMapper mapper,
        ILogger<GetAllProceduresQueryHandler> logger) : IQueryHandler<GetAllProceduresQuery, IEnumerable<ProcedureDto>>
    {

        public async Task<Result<IEnumerable<ProcedureDto>>> Handle(GetAllProceduresQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var procedures = await procedureRepository.GetAllFilteredProcedures(null, null,
                    request.From, request.To,
                    request.MinNumberOfAssistants, request.MaxNumberOfAssistants,
                    request.DoctorName, request.AssistantNames,
                    request.ClinicName, request.ClinicAddress);
                if (procedures.Count == 0)
                {
                    return Result.Failure<IEnumerable<ProcedureDto>>(["Data not Found"]);
                }
                var res = mapper.Map<IEnumerable<ProcedureDto>>(procedures);
                return Result.Success(res);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Couldn't get procedures");
                return Result.Failure<IEnumerable<ProcedureDto>>(["Something went wrong"]);
            }
        }
    }
}
