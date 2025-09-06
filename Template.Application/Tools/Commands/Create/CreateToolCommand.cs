using Microsoft.AspNetCore.Http;
using Template.Application.Abstraction.Commands;

namespace Template.Application.Tools.Commands.Create;

public class CreateToolCommand : ICommand<int>
{
    public string Name { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Thickness { get; set; }
    public int Quantity { get; set; }
    public int? KitId { get; set; }
    public int CategoryId { get; set; }
    public IFormFile? Image { get; set; }
}
