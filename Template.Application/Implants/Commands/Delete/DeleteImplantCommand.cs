using MediatR;

namespace Template.Application.Implants.Commands.Delete;

public class DeleteImplantCommand(int id) : IRequest
{
	public int Id { get; } = id;
}
