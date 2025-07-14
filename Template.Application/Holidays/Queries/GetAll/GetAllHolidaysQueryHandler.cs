using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Queries;
using Template.Application.Holidays.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Holidays.Queries.GetAll
{
    public class GetAllHolidaysQueryHandler(ILogger<GetAllHolidaysQueryHandler> logger, IMapper mapper, 
        IHolidayRepository holidayRepository) : IQueryHandler<GetAllHolidaysQuery, IEnumerable<HolidayDto>>
    {
        public async Task<Result<IEnumerable<HolidayDto>>> Handle(GetAllHolidaysQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all holidays");

            var holidays = await holidayRepository.GetAllAsync();
            var holidaysDtos = mapper.Map<IEnumerable<HolidayDto>>(holidays);
            return Result.Success(holidaysDtos);
        }
    }
}
