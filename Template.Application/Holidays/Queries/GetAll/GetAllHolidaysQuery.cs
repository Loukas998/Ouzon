using MediatR;
using Template.Application.Holidays.Dtos;

namespace Template.Application.Holidays.Queries.GetAll;

public class GetAllHolidaysQuery : IRequest<IEnumerable<HolidayDto>>
{
}
