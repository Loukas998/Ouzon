using Template.Domain.Enums;

namespace Template.Application.Holidays.Dtos;

public class HolidayDto
{
    public int Id { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? Note { get; set; }
    public HolidayStatus Status { get; set; }
}
