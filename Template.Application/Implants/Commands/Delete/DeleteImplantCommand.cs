using MediatR;
using Template.Application.Abstraction.Commands;

namespace Template.Application.Implants.Commands.Delete;

public class DeleteImplantCommand(int id) : ICommand
{
	public int Id { get; } = id;
}
