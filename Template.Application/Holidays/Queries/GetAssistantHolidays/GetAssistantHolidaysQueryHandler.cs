using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Queries;
using Template.Application.Holidays.Dtos;
using Template.Application.Holidays.Queries.GetAll;
using Template.Application.Users;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Holidays.Queries.GetAssistantHolidays
{
    public class GetAssistantHolidaysQueryHandler(ILogger<GetAllHolidaysQueryHandler> logger, IMapper mapper,
        IHolidayRepository holidayRepository, IUserContext userContext) : IQueryHandler<GetAssistantHolidaysQuery, List<HolidayDto>>
    {
        public async Task<Result<List<HolidayDto>>> Handle(GetAssistantHolidaysQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all holidays");
            var currentUser = userContext.GetCurrentUser();
            if(currentUser == null)
            {
                return Result.Failure<List<HolidayDto>>(["No one is Logged in"]);
            }
            var holidays = await holidayRepository.GetAllHolidaysWithFilter(null,null,currentUser.Id);
            var holidaysDtos = mapper.Map<List<HolidayDto>>(holidays);
            return Result.Success(holidaysDtos);
        }
    }
}
