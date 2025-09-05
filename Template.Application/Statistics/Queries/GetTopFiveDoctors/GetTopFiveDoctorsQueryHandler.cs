using AutoMapper;
using MediatR;
using Template.Application.Users.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Statistics.Queries.GetTopFiveDoctors;

public class GetTopFiveDoctorsQueryHandler(IStatisticsRepository statisticsRepository, IMapper mapper)
    : IRequestHandler<GetTopFiveDoctorsQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetTopFiveDoctorsQuery request, CancellationToken cancellationToken)
    {
        var doctors = await statisticsRepository.GetTopFiveDoctors();
        return mapper.Map<IEnumerable<UserDto>>(doctors);
    }
}
