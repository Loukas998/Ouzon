using AutoMapper;
using MediatR;
using Template.Application.Users.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Statistics.Queries.GetTopFiveAssistantsByRatings;

public class GetTopFiveAssistantsByRatingsQueryHandler(IStatisticsRepository statisticsRepository, IMapper mapper)
    : IRequestHandler<GetTopFiveAssistantsByRatingsQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetTopFiveAssistantsByRatingsQuery request, CancellationToken cancellationToken)
    {
        var assistants = await statisticsRepository.GetTopFiveAssistantsByRatings();
        return mapper.Map<IEnumerable<UserDto>>(assistants);
    }
}
