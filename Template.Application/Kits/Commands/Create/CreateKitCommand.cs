using MediatR;

namespace Template.Application.Kits.Commands.Create;

public class CreateKitCommand : IRequest<int>
{
	public string Name { get; set; } = default!;
}
