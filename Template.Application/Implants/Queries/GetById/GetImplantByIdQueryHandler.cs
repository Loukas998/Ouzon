using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Queries;
using Template.Application.Implants.Dtos;
using Template.Application.Tools.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Implants.Queries.GetById;

public class GetImplantByIdQueryHandler(ILogger<GetImplantByIdQueryHandler> logger, IMapper mapper,
	IImplantRepository implantRepository) : IQueryHandler<GetImplantByIdQuery, ImplantDto>
{
	public async Task<Result<ImplantDto>> Handle(GetImplantByIdQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting implant with id: {@Id}", request.Id);

		var implant = await implantRepository.FindByIdAsync(request.Id);
		if (implant == null) 
		{
			return Result.Failure<ImplantDto>(["Data not Found"]);
		}

		var implantDto = mapper.Map<ImplantDto>(implant);
		return Result.Success(implantDto);
	}
}
