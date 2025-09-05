using MediatR;
using Template.Application.Users.Dtos;

namespace Template.Application.Statistics.Queries.GetTopFiveDoctors;

public class GetTopFiveDoctorsQuery : IRequest<IEnumerable<UserProcedureCountDto>>
{
}
