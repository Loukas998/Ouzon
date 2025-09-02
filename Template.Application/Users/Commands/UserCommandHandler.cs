using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities;
using Template.Domain.Entities.AuthEntities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Enums;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands;

public class RegisterUserCommandHandler(IMapper mapper,
        ILogger<RegisterUserCommandHandler> logger,
        IAccountRepository accountRepository,
        IFileService fileService) : ICommandHandler<RegisterUserCommand, IEnumerable<IdentityError>>
{
    public async Task<Result<IEnumerable<IdentityError>>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request);
        user.UserName = request.UserName;
        if (request.Role == EnumRoleNames.User.ToString())
        {
            user.Clinic = new Domain.Entities.Users.Clinic()
            {
                Address = request.Address,
                Longtitude = request.Longtitude,
                Latitude = request.Latitude,
                Name = request.ClinicName,

            };
        }

        if (request.ProfilePicture != null)
        {
            try
            {
                user.ProfileImagePath = await fileService.SaveFileAsync(request.ProfilePicture, "Images/Users", [".jpg", ".png", ".webp", ".jpeg"]);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Failure<IEnumerable<IdentityError>>([ex.Message]);
            }
        }

        var errors = await accountRepository.Register(user, request.Password, request.Role);
        return Result.Success(errors);
    }
}
public class LoginUserCommandHandler(ILogger<LoginUserCommandHandler> logger,
        ITokenRepository tokenRepository,
        IAccountRepository accountRepository,
        UserManager<User> userManager, IDeviceRepository deviceRepository) : ICommandHandler<LoginUserCommand, AuthResponse?>
{
    public async Task<Result<AuthResponse?>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("looking for user with email: {Email}", request.Email);
        var tokenResponse = await accountRepository.LoginUser(request.Email, request.Password, request.DeviceToken);
        if (tokenResponse != null)
        {
            return Result.Success(tokenResponse);
        }
        return Result.Failure<AuthResponse?>(["Email or Password is Wrong"]);
    }
}
