using Template.Domain.Enums;

namespace Template.Domain.Entities.Schedule;

public class Holiday : BaseEntity
{
    public int Id { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? Note { get; set; }
    public HolidayStatus Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string UserId { get; set; } = default!;
    public User User { get; set; }
}
