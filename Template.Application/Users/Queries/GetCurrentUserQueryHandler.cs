using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Users.Dtos;
using Template.Domain.Entities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Users.Queries;

class GetCurrentUserQueryHandler(ILogger<GetCurrentUserQueryHandler>logger,IMapper mapper,IUserContext userContext,IAccountRepository accountRepository): IRequestHandler<GetCurrentUserQuery, Result<UserDto>>
{
    public async Task<Result<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var current_User = userContext.GetCurrentUser();
        var fullUser = await accountRepository.GetUserAsync(current_User.Id);
        if(fullUser == null)
        {
            return Result.Failure<UserDto>(["User not found"]);
        }
        var response = mapper.Map<UserDto>(fullUser);
        response.Role = current_User.Roles.First();
        return Result.Success(response);
    }
}
