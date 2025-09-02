using MediatR;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.VerifyForgotPasswordOtp;

public class VerifyForgotPasswordOtpCommandHandler(IAccountRepository accountRepository)
    : IRequestHandler<VerifyForgotPasswordOtpCommand, bool>
{
    public async Task<bool> Handle(VerifyForgotPasswordOtpCommand request, CancellationToken cancellationToken)
    {
        return await accountRepository.VerifyForgotPasswordOtp(request.Code);
    }
}
