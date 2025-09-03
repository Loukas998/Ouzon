using MediatR;
using Microsoft.AspNetCore.Identity;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.ResetPassword;

public class ResetPasswordCommandHandler(IAccountRepository accountRepository) : IRequestHandler<ResetPasswordCommand, IEnumerable<IdentityError>>
{
    public async Task<IEnumerable<IdentityError>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var otpCorrect = await accountRepository.VerifyForgotPasswordOtp(request.Otp);
        if (!otpCorrect)
        {
            return new List<IdentityError>() { new()
            {
                Code = "Otp Incorrect",
                Description = "Verification code is incorrect"
            } };
        }
        var errors = await accountRepository.ResetPassword(request.Email, request.NewPassword);
        return errors;

    }
}
