using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Entities.Schedule;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Holidays.Commands.Delete;

public class DeleteHolidayCommandHandler(ILogger<DeleteHolidayCommandHandler> logger, IMapper mapper, 
    IHolidayRepository holidayRepository) : ICommandHandler<DeleteHolidayCommand>
{
    public async Task<Result> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting holiday with id: {Id}", request.HolidayId);

        var holiday = await holidayRepository.FindByIdAsync(request.HolidayId);
        if (holiday == null)
        {
            logger.LogError("Couldn't Find Entity");
            return Result.Failure([nameof(Holiday) + request.HolidayId.ToString()]);
        }

        await holidayRepository.DeleteAsync(holiday);
        return Result.Success();
    }
}
