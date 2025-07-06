namespace Template.Domain.Entities.Notifications;

public class Device
{
    public int Id { get; set; }
    public string? DeviceToken { get; set; }
    public DateTime? LastLoggedInAt { get; set; }
    public bool? OptIn { get; set; }
    public string? UserId { get; set; }
    public List<Notification> Notifications { get; set; } = [];
}
