using AutoMapper;
using Microsoft.AspNetCore.Http;
using Template.Application.Users.Dtos;
using Template.Domain.Entities;

namespace Template.Application.Users.UserProfile;

public class DetailedUserImageUrlResolver(IHttpContextAccessor httpContextAccessor) : IValueResolver<User, UserDetailedDto, string>
{
    public string Resolve(User source, UserDetailedDto destination, string destMember, ResolutionContext context)
    {
        var request = httpContextAccessor.HttpContext?.Request;
        if (request == null || string.IsNullOrEmpty(source.ProfileImagePath)) return "";

        return $"{request.Scheme}://{request.Host}/{source.ProfileImagePath.Replace("\\", "/")}";
    }
}
