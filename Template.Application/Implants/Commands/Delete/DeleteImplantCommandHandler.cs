using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Application.Implants.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Implants.Commands.Delete;

public class DeleteImplantCommandHandler(ILogger<DeleteImplantCommandHandler> logger, IMapper mapper,
	IImplantRepository implantRepository) : ICommandHandler<DeleteImplantCommand>
{
	public async Task<Result> Handle(DeleteImplantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting implant with id: {@Id}", request.Id);

		var implant = await implantRepository.FindByIdAsync(request.Id);
		if (implant == null)
		{
			return Result.Failure(["Data not Found"]);
		}

		await implantRepository.DeleteAsync(implant);
		return Result.Success();
	}
}
