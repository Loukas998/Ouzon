using MediatR;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.ChangePassword;

public class ChangePasswordCommandHandler(IAccountRepository accountRepository, IUserContext userContext)
    : IRequestHandler<ChangePasswordCommand, bool>
{
    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        var existingUser = await accountRepository.GetUserDetails(user.Id);

        if (existingUser != null)
        {
            return await accountRepository.UpdatePassword(existingUser, request.OldPassword, request.NewPassword);
        }
        return false;
    }
}
