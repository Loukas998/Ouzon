﻿namespace Template.Domain.Entities.Notifications;

public class Notification
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public int? DeviceId { get; set; }
    public Device Device { get; set; }
    public bool Read { get; set; }
}
