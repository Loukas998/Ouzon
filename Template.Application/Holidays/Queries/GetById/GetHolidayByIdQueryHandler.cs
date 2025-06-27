using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Holidays.Dtos;
using Template.Domain.Entities.Schedule;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Holidays.Queries.GetById;

public class GetHolidayByIdQueryHandler(ILogger<GetHolidayByIdQueryHandler> logger, IMapper mapper, 
    IHolidayRepository holidayRepository) : IRequestHandler<GetHolidayByIdQuery, HolidayDto>
{
    public async Task<HolidayDto> Handle(GetHolidayByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting holiday with id: {Id}", request.HolidayId);

        var holiday = await holidayRepository.FindByIdAsync(request.HolidayId);
        if (holiday == null)
        {
            throw new NotFoundException(nameof(Holiday), request.HolidayId.ToString());
        }

        var holidayDto = mapper.Map<HolidayDto>(holiday);
        return holidayDto;
    }
}
