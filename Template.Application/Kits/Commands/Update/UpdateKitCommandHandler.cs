using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Materials;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Kits.Commands.Update;

public class UpdateKitCommandHandler(ILogger<UpdateKitCommandHandler> logger, IMapper mapper,
	IKitRepository kitRepository) : IRequestHandler<UpdateKitCommand>
{
	public async Task Handle(UpdateKitCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Updating kit with id: {KitId}, the new product: {@Kit}",
				request.KitId, request);

		var kit = await kitRepository.FindByIdAsync(request.KitId);
		if (kit == null)
		{
			throw new NotFoundException(nameof(Kit), request.KitId.ToString());
		}

		mapper.Map(request, kit);
		await kitRepository.SaveChangesAsync();
	}
}
