using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Queries;
using Template.Application.Kits.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Kits.Queries.GetById;

public class GetKitByIdQueryHandler(ILogger<GetKitByIdQueryHandler> logger, IMapper mapper, 
	IKitRepository kitRepository) : IQueryHandler<GetKitByIdQuery, KitDto>
{
	public async Task<Result<KitDto>> Handle(GetKitByIdQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting kit by id: {@Id}", request.Id);

		var kit = await kitRepository.FindByIdAsync(request.Id);
		if(kit == null)
		{
			return Result.Failure<KitDto>(["Data Not Found"]);
		}

		var kitDto = mapper.Map<KitDto>(kit);
		return Result.Success(kitDto);
	}
}
