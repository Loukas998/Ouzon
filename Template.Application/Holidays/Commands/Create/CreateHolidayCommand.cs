using MediatR;
using Template.Domain.Enums;

namespace Template.Application.Holidays.Commands.Create;

public class CreateHolidayCommand : IRequest<int>
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? Note { get; set; }
    public HolidayStatus Status { get; set; }
}
