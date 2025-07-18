using MediatR;
using Template.Application.Abstraction.Commands;

namespace Template.Application.Kits.Commands.Delete;

public class DeleteKitCommand(int id) : ICommand
{
	public int Id { get; } = id;
}
