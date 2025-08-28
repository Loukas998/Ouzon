using AutoMapper;
using Microsoft.AspNetCore.Http;
using Template.Domain.Entities.Materials;

namespace Template.Application.Implants.Dtos;

public class ImplantImageUrlResolver(IHttpContextAccessor httpContextAccessor) : IValueResolver<Implant, ImplantDto, string>
{
    public string Resolve(Implant source, ImplantDto destination, string destMember, ResolutionContext context)
    {
        var request = httpContextAccessor.HttpContext?.Request;
        if (request == null || string.IsNullOrEmpty(source.ImagePath)) return "";

        return $"{request.Scheme}://{request.Host}/{source.ImagePath.Replace("\\", "/")}";
    }
}
