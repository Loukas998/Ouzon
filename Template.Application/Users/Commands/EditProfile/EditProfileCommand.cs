using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Template.Application.Abstraction.Commands;
using Template.Application.Users.Dtos;

namespace Template.Application.Users.Commands.EditProfile;

public class EditProfileCommand : ICommand<UserDto>
{
    public string? UserName { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
    public IFormFile? Image { get; set; }

    public string? Address { get; set; }
    public string? Name { get; set; }
    public string? ClinicName { get; set; }
    public float Longtitude { get; set; }
    public float Latitude { get; set; }
}
