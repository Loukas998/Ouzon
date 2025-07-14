using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Entities.Schedule;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Holidays.Commands.ChangeStatus;

public class ChangeHolidayStatusCommandHandler(ILogger<ChangeHolidayStatusCommandHandler> logger, IHolidayRepository holidayRepository, 
    IMapper mapper) : ICommandHandler<ChangeHolidayStatusCommand>
{
    public async Task<Result> Handle(ChangeHolidayStatusCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Changing holiday status of id: {Id} with data: {@Request}", request.HolidayId, request);

        var holiday = await holidayRepository.FindByIdAsync(request.HolidayId);
        if (holiday == null)
        {
            return Result.Failure(["Entity not found"]);
        }

        mapper.Map(request, holiday);
        await holidayRepository.SaveChangesAsync();
        return Result.Success();
    }
}
