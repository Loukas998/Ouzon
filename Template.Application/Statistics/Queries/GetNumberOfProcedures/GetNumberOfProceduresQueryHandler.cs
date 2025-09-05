using MediatR;
using Template.Domain.Repositories;

namespace Template.Application.Statistics.Queries.GetNumberOfProcedures;

public class GetNumberOfProceduresQueryHandler(IStatisticsRepository statisticsRepository)
    : IRequestHandler<GetNumberOfProceduresQuery, int>
{
    public async Task<int> Handle(GetNumberOfProceduresQuery request, CancellationToken cancellationToken)
    {
        var number = await statisticsRepository.GetNumberOfProcedures(
            request.Month, request.Year,
            request.StartDate, request.EndDate);

        return number;
    }
}
