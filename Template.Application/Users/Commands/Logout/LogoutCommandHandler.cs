using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.Logout
{
    public class LogoutCommandHandler(IUserContext userContext, IAccountRepository accountRepository) : ICommandHandler<LogoutCommand>
    {
        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();
            if (currentUser == null)
            {
                return Result.Failure(["User not logged in to log out"]);
            }
            var result = await accountRepository.UpdateSecurityStampAsync(currentUser.Id);
            if (result.Succeeded)
            {
                return Result.Success();
            }
            List<string> errors = [];
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }
            return Result.Failure(errors);
        }
    }
}
