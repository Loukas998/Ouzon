using MediatR;
using Template.Application.Users.Dtos;

namespace Template.Application.Statistics.Queries.GetTopFiveAssistantsByAssignments;

public class GetTopFiveAssistantsByAssignmentsQuery : IRequest<IEnumerable<UserDto>>
{
}
