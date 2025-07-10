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
{
    public async Task<AuthResponse?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("looking for user with email: {Email}", request.Email);
       var tokenResponse = await accountRepository.LoginUser(request.Email, request.Password);
        if(tokenResponse != null)
        {
            return tokenResponse;
        }
        return null;
    }
}
