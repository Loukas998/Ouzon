using MediatR;

namespace Template.Application.Kits.Commands.Delete;

public class DeleteKitCommand(int id) : IRequest
{
	public int Id { get; } = id;
}
