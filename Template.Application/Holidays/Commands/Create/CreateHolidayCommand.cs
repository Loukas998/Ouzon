using MediatR;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Enums;

namespace Template.Application.Holidays.Commands.Create;

public class CreateHolidayCommand : ICommand<int>
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? Note { get; set; }
  //  public HolidayStatus Status { get; set; }
}
