using Template.Application.Abstraction.Commands;

namespace Template.Application.Holidays.Commands.Delete;

public class DeleteHolidayCommand(int holidayId) : ICommand
{
    public int HolidayId { get; } = holidayId;
}
