using AutoMapper;
using Microsoft.AspNetCore.Http;
using Template.Domain.Entities.Materials;

namespace Template.Application.Kits.Dtos;

public class KitImageUrlResolver(IHttpContextAccessor httpContextAccessor) : IValueResolver<Kit, KitDto, string>
{
    public string Resolve(Kit source, KitDto destination, string destMember, ResolutionContext context)
    {
        var request = httpContextAccessor.HttpContext?.Request;
        if (request == null || string.IsNullOrEmpty(source.ImagePath)) return "";

        return $"{request.Scheme}://{request.Host}/{source.ImagePath.Replace("\\", "/")}";
    }
}
