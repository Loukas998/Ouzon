using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities;
using Template.Domain.Entities.AuthEntities;
using Template.Domain.Entities.Notifications;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands;

public class RegisterUserCommandHandler(IMapper mapper,
        IAccountRepository accountRepository) : IRequestHandler<RegisterUserCommand, IEnumerable<IdentityError>>
{
    public async Task<IEnumerable<IdentityError>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request);
        user.UserName = request.UserName;
        user.Clinic = new Domain.Entities.Users.Clinic()
        {
            Address = request.Address,
            Longtitude = request.Longtitude,
            Latitude =request.Latitude,
            Name = request.ClinicName,

        };
        var errors = await accountRepository.Register(user, request.Password,request.Role);
        return errors;
    }
}
public class LoginUserCommandHandler(ILogger<LoginUserCommandHandler> logger,
        ITokenRepository tokenRepository,
        IAccountRepository accountRepository,
        UserManager<User> userManager) : IRequestHandler<LoginUserCommand, AuthResponse?>
        UserManager<User> userManager, IDeviceRepository deviceRepository) : IRequestHandler<LoginUserCommand, AuthResponse?>
{
    public async Task<AuthResponse?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("looking for user with email: {Email}", request.Email);
       var tokenResponse = await accountRepository.LoginUser(request.Email, request.Password);
        if(tokenResponse != null)
        {
            var existingDevice = await deviceRepository.SearchAsync(request.DeviceToken, null, null, null, null);
            if (existingDevice != null)
            {
                existingDevice[0].LastLoggedInAt = DateTime.UtcNow;
                await deviceRepository.SaveChangesAsync();
            } else {

                var device = new Device()
                {
                    LastLoggedInAt = DateTime.UtcNow,
                    DeviceToken = request.DeviceToken,
                    UserId = user.Id,
                    OptIn = true
                };
                await deviceRepository.AddAsync(device);
            }

            var token = await tokenRepository.GenerateToken(user.Id);
            return token;
            return tokenResponse;
        }
        return null;
    }
}
