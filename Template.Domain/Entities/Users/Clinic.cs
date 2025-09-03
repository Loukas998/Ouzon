namespace Template.Domain.Entities.Users;

public class Clinic : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public float Longtitude { get; set; }
    public float Latitude { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
}
