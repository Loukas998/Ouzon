using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Implants.Dtos;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Implants.Commands.Delete;

public class DeleteImplantCommandHandler(ILogger<DeleteImplantCommandHandler> logger, IMapper mapper,
	IImplantRepository implantRepository) : IRequestHandler<DeleteImplantCommand>
{
	public async Task Handle(DeleteImplantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting implant with id: {@Id}", request.Id);

		var implant = await implantRepository.FindByIdAsync(request.Id);
		if (implant == null)
		{
			throw new NotFoundException(nameof(implant), request.Id.ToString());
		}

		await implantRepository.DeleteAsync(implant);
	}
}
