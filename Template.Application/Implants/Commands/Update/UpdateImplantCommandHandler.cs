using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Commands;
using Template.Domain.Entities.Materials;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Implants.Commands.Update;

public class UpdateImplantCommandHandler(ILogger<UpdateImplantCommandHandler> logger, IMapper mapper,
	IImplantRepository implantRepository) : ICommandHandler<UpdateImplantCommand>
{
	public async Task<Result> Handle(UpdateImplantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Updating product with id: {ImplantId}, the new product: {@Implant}",
				request.ImplantId, request);

		var implant = await implantRepository.FindByIdAsync(request.ImplantId);
		if (implant == null)
		{
			return Result.Failure(["Data not Found"]);
		}

		mapper.Map(request, implant);
		await implantRepository.UpdateAsync(implant);
		return Result.Success();
	}
}
