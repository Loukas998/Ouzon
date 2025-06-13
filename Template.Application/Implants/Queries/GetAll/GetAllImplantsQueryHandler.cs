using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Implants.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Implants.Queries.GetAll;

public class GetAllImplantsQueryHandler(ILogger<GetAllImplantsQueryHandler> logger, IMapper mapper, 
	IImplantRepository implantRepository) : IRequestHandler<GetAllImplantsQuery, IEnumerable<ImplantDto>>
{
	public async Task<IEnumerable<ImplantDto>> Handle(GetAllImplantsQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting all implants");

		var implants = await implantRepository.GetAllAsync();

		var implantDtos = mapper.Map<IEnumerable<ImplantDto>>(implants);
		return implantDtos;
	}
}
