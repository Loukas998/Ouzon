using AutoMapper;
using Microsoft.AspNetCore.Http;
using Template.Application.Users.Dtos;
using Template.Domain.Entities;

namespace Template.Application.Users.UserProfile
{
    public class UserRoleImageUrlResolver(IHttpContextAccessor httpContextAccessor) : IValueResolver<(User, string), UserDto, string>
    {
        public string Resolve((User, string) src, UserDto destination, string destMember, ResolutionContext context)
        {
            var request = httpContextAccessor.HttpContext?.Request;
            if (request == null || string.IsNullOrEmpty(src.Item1.ProfileImagePath)) return "";

            return $"{request.Scheme}://{request.Host}/{src.Item1.ProfileImagePath.Replace("\\", "/")}";
        }
    }
}