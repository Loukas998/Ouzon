using MediatR;
using System.Windows.Input;
using Template.Application.Abstraction.Commands;

namespace Template.Application.Kits.Commands.Create;

public class CreateKitCommand : ICommand<int>
{
	public string Name { get; set; } = default!;
	public bool IsMainKit  { get; set; } = false;
}
