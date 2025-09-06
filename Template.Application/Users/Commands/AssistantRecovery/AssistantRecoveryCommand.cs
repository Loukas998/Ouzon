using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Template.Application.Users.Commands.AssistantRecovery;

public class AssistantRecoveryCommand : IRequest<IdentityResult>
{
    public string UserId { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}
