using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Template.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<bool>
{
    [Required]
    public string NewPassword { get; set; }
    [Required]
    [Compare("NewPassword", ErrorMessage = "Password do not match")]
    public string ConfirmNewPassword { get; set; }
    [Required]
    public string OldPassword { get; set; }
}
