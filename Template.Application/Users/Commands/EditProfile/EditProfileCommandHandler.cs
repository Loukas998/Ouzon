using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Application.Users.Dtos;
using Template.Domain.Entities;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Users.Commands.EditProfile;

public class EditProfileCommandHandler(IUserContext userContext, ILogger<EditProfileCommandHandler> logger,
    IAccountRepository accountRepository, IMapper mapper, IFileService fileService)
    : ICommandHandler<EditProfileCommand, UserDto>
{
    public async Task<Result<UserDto>> Handle(EditProfileCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        var user = await accountRepository.GetUserAsync(currentUser.Id, currentUser.Roles.ElementAt(0).Equals("AssistantDoctor"));
        if (user == null)
        {
            throw new NotFoundException(nameof(User), currentUser.Id);
        }
        if (request.Email != null)
        {
            var existsWithSameEmail = await accountRepository.FindUserByEmail(request.Email);
            if (existsWithSameEmail != null)
            {
                throw new InvalidOperationException($"User with The Email {request.Email} already Exists");
            }
        }
        if (request.UserName != null)
        {
            var existsWithSameUserName = await accountRepository.FindUserByUserName(request.UserName);
            if (existsWithSameUserName != null)
            {
                throw new InvalidOperationException($"User with The username {request.UserName} already Exists");
            }
        }
        user.UserName = request.UserName ?? user.UserName;
        user.Email = request.Email ?? user.Email;
        user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
        if (currentUser.Roles.Contains("User"))
        {
            user.Clinic.Address = request.Address ?? user.Clinic.Address;
            user.Clinic.Latitude = request.Latitude != 0 ? request.Latitude : user.Clinic.Latitude;
            user.Clinic.Longtitude = request.Longtitude != 0 ? request.Longtitude : user.Clinic.Longtitude;
            user.Clinic.Name = request.Name ?? user.Clinic.Name;
        }
        if (request.Image != null)
        {
            if (user.ProfileImagePath != null)
            {
                fileService.DeleteFile(user.ProfileImagePath);
                user.ProfileImagePath = null;
            }


            user.ProfileImagePath = fileService.SaveFile(request.Image, "Images/Users", [".jpg", ".png", ".webp", ".jpeg"]);

            //catch (Exception ex)
            //{
            //    logger.LogError(ex, ex.Message);
            //    return Result.Failure<UserDto>([ex.Message]);
            //}
        }

        var updated = await accountRepository.UpdateUserAsync(user);
        var result = mapper.Map<UserDto>(updated);
        return Result.Success(result);
    }
}
