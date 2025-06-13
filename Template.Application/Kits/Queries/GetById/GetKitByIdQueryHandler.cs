using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Kits.Dtos;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Kits.Queries.GetById;

public class GetKitByIdQueryHandler(ILogger<GetKitByIdQueryHandler> logger, IMapper mapper, 
	IKitRepository kitRepository) : IRequestHandler<GetKitByIdQuery, KitDto>
{
	public async Task<KitDto> Handle(GetKitByIdQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting kit by id: {@Id}", request.Id);

		var kit = await kitRepository.FindByIdAsync(request.Id);
		if(kit == null)
		{
			throw new NotFoundException(nameof(kit), request.Id.ToString());
		}

		var kitDto = mapper.Map<KitDto>(kit);
		return kitDto;
	}
}
