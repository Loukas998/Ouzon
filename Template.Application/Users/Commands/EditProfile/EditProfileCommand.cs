using MediatR;
using Microsoft.AspNetCore.Http;
using Template.Application.Users.Dtos;

namespace Template.Application.Users.Commands.EditProfile;

public class EditProfileCommand : IRequest<UserDto>
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public IFormFile? Image { get; set; }

    public string? Address { get; set; }
    public string? Name { get; set; }
    public string? ClinicName { get; set; }
    public float Longtitude { get; set; }
    public float Latitude { get; set; }
}
