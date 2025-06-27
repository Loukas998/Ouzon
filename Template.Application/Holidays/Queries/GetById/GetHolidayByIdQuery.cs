using MediatR;
using Template.Application.Holidays.Dtos;

namespace Template.Application.Holidays.Queries.GetById;

public class GetHolidayByIdQuery(int holidayId) : IRequest<HolidayDto>
{
    public int HolidayId { get; } = holidayId;
}
