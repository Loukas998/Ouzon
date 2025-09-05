using MediatR;
using Template.Application.Users.Dtos;

namespace Template.Application.Statistics.Queries.GetTopFiveAssistantsByRatings;

public class GetTopFiveAssistantsByRatingsQuery : IRequest<IEnumerable<UserDto>>
{
}
