using MediatR;

namespace Template.Application.Users.Commands.VerifyForgotPasswordOtp;

public class VerifyForgotPasswordOtpCommand : IRequest<bool>
{
    public string Code { get; set; } = default!;
}
