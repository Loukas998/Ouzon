using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Users;
using Template.Domain.Entities.Schedule;
using Template.Domain.Repositories;

namespace Template.Application.Holidays.Commands.Create;

public class CreateHolidayCommandHandler(ILogger<CreateHolidayCommandHandler> logger, IHolidayRepository holidayRepository, 
    IMapper mapper, IUserContext userContext) : IRequestHandler<CreateHolidayCommand, int>
{
    public async Task<int> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new holiday request {@Request}", request);
        var holiday = mapper.Map<Holiday>(request);
        holiday.UserId = userContext.GetCurrentUser()?.Id;
        var result = await holidayRepository.AddAsync(holiday);
        return result.Id;
    }
}
