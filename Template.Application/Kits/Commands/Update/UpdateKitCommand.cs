using MediatR;
using Microsoft.AspNetCore.Http;

namespace Template.Application.Kits.Commands.Update;

public class UpdateKitCommand(int kitId) : IRequest
{
    public int KitId { get; } = kitId;
    public string? Name { get; set; }
    public IFormFile? Image { get; set; }
}
