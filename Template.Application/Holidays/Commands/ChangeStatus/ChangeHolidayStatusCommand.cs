using MediatR;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Enums;

namespace Template.Application.Holidays.Commands.ChangeStatus;

public class ChangeHolidayStatusCommand : ICommand
{
    public int HolidayId { get; set; }
    public string? Note { get; set; }
    public HolidayStatus NewStatus { get; set; }
}
