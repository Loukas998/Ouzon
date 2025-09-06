using MediatR;
using Microsoft.AspNetCore.Identity;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.AssistantRecovery;

public class AssistantRecoveryCommandHandler(IAccountRepository accountRepository)
    : IRequestHandler<AssistantRecoveryCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(AssistantRecoveryCommand request, CancellationToken cancellationToken)
    {
        return await accountRepository.AssistantRecoveryPassword(request.UserId, request.NewPassword);
    }
}
