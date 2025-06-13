using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Kits.Commands.Delete;

public class DeleteKitCommandHandler(ILogger<DeleteKitCommandHandler> logger,
	IKitRepository kitRepository) : IRequestHandler<DeleteKitCommand>
{
	public async Task Handle(DeleteKitCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Deleting kit by id: {@Id}", request.Id);

		var kit = await kitRepository.FindByIdAsync(request.Id);
		if (kit == null)
		{
			throw new NotFoundException(nameof(kit), request.Id.ToString());
		}

		await kitRepository.DeleteAsync(kit);
	}
}
