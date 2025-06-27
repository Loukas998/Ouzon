using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Enums;

namespace Template.Domain.Entities.Schedule;

public class Holiday
{
    public int Id { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? Note { get; set; }
    public HolidayStatus Status { get; set; }

    public string UserId { get; set; } = default!;
}
