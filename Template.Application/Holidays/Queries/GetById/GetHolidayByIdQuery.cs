using MediatR;
using Template.Application.Abstraction.Queries;
using Template.Application.Holidays.Dtos;

namespace Template.Application.Holidays.Queries.GetById;

public class GetHolidayByIdQuery(int holidayId) : IQuery<HolidayDto>
{
    public int HolidayId { get; } = holidayId;
}
