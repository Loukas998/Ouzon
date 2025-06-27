using MediatR;

namespace Template.Application.Holidays.Commands.Delete;

public class DeleteHolidayCommand(int holidayId) : IRequest
{
    public int HolidayId { get; } = holidayId;
}
