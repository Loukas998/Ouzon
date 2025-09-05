using MediatR;

namespace Template.Application.Statistics.Queries.GetNumberOfUsersInEachRole;

public class GetNumberOfUsersInEachRoleQuery : IRequest<Dictionary<string, int>>
{
}
