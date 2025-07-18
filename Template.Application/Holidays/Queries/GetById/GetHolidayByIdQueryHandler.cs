using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Queries;
using Template.Application.Holidays.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Entities.Schedule;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Holidays.Queries.GetById;

public class GetHolidayByIdQueryHandler(ILogger<GetHolidayByIdQueryHandler> logger, IMapper mapper, 
    IHolidayRepository holidayRepository) : IQueryHandler<GetHolidayByIdQuery, HolidayDto>
{
    public async Task<Result<HolidayDto>> Handle(GetHolidayByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting holiday with id: {Id}", request.HolidayId);

        var holiday = await holidayRepository.FindByIdAsync(request.HolidayId);
        if (holiday == null)
        {
            return Result.Failure<HolidayDto>(["Data not Found"]);
        }

        var holidayDto = mapper.Map<HolidayDto>(holiday);
        return Result.Success(holidayDto);
    }
}
