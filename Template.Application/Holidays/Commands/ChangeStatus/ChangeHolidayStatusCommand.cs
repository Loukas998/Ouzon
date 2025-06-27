using MediatR;
using Template.Domain.Enums;

namespace Template.Application.Holidays.Commands.ChangeStatus;

public class ChangeHolidayStatusCommand : IRequest
{
    public int HolidayId { get; set; }
    public string? Note { get; set; }
    public HolidayStatus NewStatus { get; set; }
}
