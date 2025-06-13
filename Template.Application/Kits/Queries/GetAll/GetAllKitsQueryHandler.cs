using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Kits.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Kits.Queries.GetAll;

public class GetAllKitsQueryHandler(IKitRepository kitRepository, IMapper mapper,
	ILogger<GetAllKitsQueryHandler> logger) : IRequestHandler<GetAllKitsQuery, IEnumerable<KitDto>>
{
	public async Task<IEnumerable<KitDto>> Handle(GetAllKitsQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting all kits");

		var kits = await kitRepository.GetAllAsync();

		var kitsDtos = mapper.Map<IEnumerable<KitDto>>(kits);
		return kitsDtos;
	}
}
