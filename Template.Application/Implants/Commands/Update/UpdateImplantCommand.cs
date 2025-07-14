using MediatR;
using Template.Application.Abstraction.Commands;

namespace Template.Application.Implants.Commands.Update;

public class UpdateImplantCommand : ICommand
{
	public int ImplantId { get; set; }
	public float Radius { get; set; }
	public float Width { get; set; }
	public float Height { get; set; }
	public int Quantity { get; set; }
	public string? Brand { get; set; }
	public string? Description { get; set; }


	public int KitId { get; set; }
}
