using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Schedule;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Holidays.Commands.Delete;

public class DeleteHolidayCommandHandler(ILogger<DeleteHolidayCommandHandler> logger, IMapper mapper, 
    IHolidayRepository holidayRepository) : IRequestHandler<DeleteHolidayCommand>
{
    public async Task Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting holiday with id: {Id}", request.HolidayId);

        var holiday = await holidayRepository.FindByIdAsync(request.HolidayId);
        if (holiday == null)
        {
            throw new NotFoundException(nameof(Holiday), request.HolidayId.ToString());
        }

        await holidayRepository.DeleteAsync(holiday);
    }
}
