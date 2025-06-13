using MediatR;

namespace Template.Application.Kits.Commands.Update;

public class UpdateKitCommand(int kitId) : IRequest
{
	public int KitId { get; } = kitId;
	public string? Name { get; set; }
}
