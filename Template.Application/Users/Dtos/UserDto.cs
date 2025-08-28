namespace Template.Application.Users.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = "User";
        public ClinicDto Clinic { get; set; }
        public int Rate { get; set; }
        public string? ProfileImagePath { get; set; }

    }
}
