using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Application.Users;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Entities.Schedule;
using Template.Domain.Repositories;

namespace Template.Application.Holidays.Commands.Create;

public class CreateHolidayCommandHandler(ILogger<CreateHolidayCommandHandler> logger, IHolidayRepository holidayRepository, 
    IMapper mapper, IUserContext userContext) : ICommandHandler<CreateHolidayCommand, int>
{
    public async Task<Result<int>> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new holiday request {@Request}", request);
        var holiday = mapper.Map<Holiday>(request);
        holiday.UserId = userContext.GetCurrentUser()?.Id;
        holiday.Status = Domain.Enums.HolidayStatus.Pending;
        var result = await holidayRepository.AddAsync(holiday);
        return Result.Success(result.Id);
    }
}
