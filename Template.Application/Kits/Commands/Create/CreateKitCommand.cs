using Microsoft.AspNetCore.Http;
using Template.Application.Abstraction.Commands;

namespace Template.Application.Kits.Commands.Create;

public class CreateKitCommand : ICommand<int>
{
    public string Name { get; set; } = default!;
    public bool IsMainKit { get; set; } = false;
    public IFormFile? Image { get; set; }
}
