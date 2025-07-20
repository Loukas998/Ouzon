using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Queries;
using Template.Application.Procedure.Dtos.MainProcedure;
using Template.Application.Users;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Procedure.Queries.AssistantProcedures
{
    public class GetAssistantProceduresQueryHandler(IProcedureRepository procedureRepository,
         IMapper mapper,
         ILogger<GetAssistantProceduresQueryHandler> logger,
         IUserContext userContext) : IQueryHandler<GetAssistantProceduresQuery, IEnumerable<ProcedureDto>>
    {

        public async Task<Result<IEnumerable<ProcedureDto>>> Handle(GetAssistantProceduresQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var currentUser = userContext.GetCurrentUser();
                var procedures = await procedureRepository.GetAllFilteredProcedures(null, currentUser.Id);
                if (procedures == null)
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
