using MediatR;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.ResetPassword;

public class ResetPasswordCommandHandler(IAccountRepository accountRepository) : IRequestHandler<ResetPasswordCommand>
{
    public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        await accountRepository.ResetPassword(request.Email, request.NewPassword);
    }
}
