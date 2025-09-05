using AutoMapper;
using MediatR;
using Template.Application.Users.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Statistics.Queries.GetTopFiveAssistantsByAssignments;

public class GetTopFiveAssistantsByAssignmentsQueryHandler(IStatisticsRepository statisticsRepository,
    IMapper mapper) : IRequestHandler<GetTopFiveAssistantsByAssignmentsQuery, IEnumerable<UserProcedureCountDto>>
{
    public async Task<IEnumerable<UserProcedureCountDto>> Handle(GetTopFiveAssistantsByAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var assistants = await statisticsRepository.GetTopFiveAssistantsByAssignments();
        return mapper.Map<IEnumerable<UserProcedureCountDto>>(assistants);
    }
}
