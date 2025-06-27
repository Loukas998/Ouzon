using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Schedule;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Holidays.Commands.ChangeStatus;

public class ChangeHolidayStatusCommandHandler(ILogger<ChangeHolidayStatusCommandHandler> logger, IHolidayRepository holidayRepository, 
    IMapper mapper) : IRequestHandler<ChangeHolidayStatusCommand>
{
    public async Task Handle(ChangeHolidayStatusCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Changing holiday status of id: {Id} with data: {@Request}", request.HolidayId, request);

        var holiday = await holidayRepository.FindByIdAsync(request.HolidayId);
        if (holiday == null)
        {
            throw new NotFoundException(nameof(Holiday), request.HolidayId.ToString());
        }

        mapper.Map(request, holiday);
        await holidayRepository.SaveChangesAsync();
    }
}
