using MediatR;
using Template.Application.Abstraction.Queries;
using Template.Application.Holidays.Dtos;

namespace Template.Application.Holidays.Queries.GetAll;

public class GetAllHolidaysQuery : IQuery<IEnumerable<HolidayDto>>
{
}
