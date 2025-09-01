using Microsoft.AspNetCore.Http;
using Template.Application.Abstraction.Commands;

namespace Template.Application.Kits.Commands.Update;

public class UpdateKitCommand(int kitId) : ICommand
{
    public int KitId { get; } = kitId;
    public string? Name { get; set; }
    public IFormFile? Image { get; set; }
}
