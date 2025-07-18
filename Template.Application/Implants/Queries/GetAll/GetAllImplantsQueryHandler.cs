using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Queries;
using Template.Application.Implants.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Implants.Queries.GetAll;

public class GetAllImplantsQueryHandler(ILogger<GetAllImplantsQueryHandler> logger, IMapper mapper, 
	IImplantRepository implantRepository) : IQueryHandler<GetAllImplantsQuery, IEnumerable<ImplantDto>>
{
	public async Task<Result<IEnumerable<ImplantDto>>> Handle(GetAllImplantsQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting all implants");

		var implants = await implantRepository.GetAllAsync();

		var implantDtos = mapper.Map<IEnumerable<ImplantDto>>(implants);
		return Result.Success(implantDtos);
	}
}
