using MediatR;
using Template.Application.Abstraction.Commands;

namespace Template.Application.Implants.Commands.Create;

public class CreateImplantCommand : ICommand<int>
{
	public float Radius { get; set; }
	public float Width { get; set; }
	public float Height { get; set; }
	public int Quantity { get; set; }
	public string? Brand { get; set; }
	public string? Description { get; set; }


	public int KitId { get; set; }
}
