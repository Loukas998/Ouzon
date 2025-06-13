using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Materials;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Implants.Commands.Update;

public class UpdateImplantCommandHandler(ILogger<UpdateImplantCommandHandler> logger, IMapper mapper,
	IImplantRepository implantRepository) : IRequestHandler<UpdateImplantCommand>
{
	public async Task Handle(UpdateImplantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Updating product with id: {ImplantId}, the new product: {@Implant}",
				request.ImplantId, request);

		var implant = await implantRepository.FindByIdAsync(request.ImplantId);
		if (implant == null)
		{
			throw new NotFoundException(nameof(Implant), request.ImplantId.ToString());
		}

		mapper.Map(request, implant);
		await implantRepository.UpdateAsync(implant);
	}
}
