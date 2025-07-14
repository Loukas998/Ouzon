using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.AuthEntities;

namespace Template.Application.Users.Commands;
public class RegisterUserCommand :IRequest<IEnumerable<IdentityError>>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Address { get; set; }
    public string? ClinicName { get; set; }
    public string? PhoneNumber { get; set; }
    public float Longtitude { get; set; }
    public float Latitude { get; set; }
    public string Role { get; set; }
    //public IFormFile? ProfilePicture { get; set; }
}

public class LoginUserCommand : ICommand<AuthResponse?>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? DeviceToken { get; set; }
}


