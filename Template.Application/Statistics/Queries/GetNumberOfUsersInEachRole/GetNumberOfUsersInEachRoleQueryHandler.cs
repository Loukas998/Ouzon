using MediatR;
using Template.Domain.Repositories;

namespace Template.Application.Statistics.Queries.GetNumberOfUsersInEachRole
{
    internal class GetNumberOfUsersInEachRoleQueryHandler(IStatisticsRepository statisticsRepository)
        : IRequestHandler<GetNumberOfUsersInEachRoleQuery, Dictionary<string, int>>
    {
        public async Task<Dictionary<string, int>> Handle(GetNumberOfUsersInEachRoleQuery request, CancellationToken cancellationToken)
        {
            return await statisticsRepository.GetNumberOfUsersInEachRoleAsync();
        }
    }
}
