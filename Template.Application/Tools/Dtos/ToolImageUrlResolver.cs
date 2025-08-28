using AutoMapper;
using Microsoft.AspNetCore.Http;
using Template.Domain.Entities.Materials;

namespace Template.Application.Tools.Dtos;

public class ToolImageUrlResolver(IHttpContextAccessor httpContextAccessor) : IValueResolver<Tool, ToolDto, string>
{
    public string Resolve(Tool source, ToolDto destination, string destMember, ResolutionContext context)
    {
        var request = httpContextAccessor.HttpContext?.Request;
        if (request == null || string.IsNullOrEmpty(source.ImagePath)) return "";

        return $"{request.Scheme}://{request.Host}/{source.ImagePath.Replace("\\", "/")}";
    }
}
