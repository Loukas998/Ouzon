using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Application.Implants.Dtos;
using Template.Domain.Exceptions;
using Template.Domain.Repositories;

namespace Template.Application.Implants.Queries.GetById;

public class GetImplantByIdQueryHandler(ILogger<GetImplantByIdQueryHandler> logger, IMapper mapper,
	IImplantRepository implantRepository) : IRequestHandler<GetImplantByIdQuery, ImplantDto>
{
	public async Task<ImplantDto> Handle(GetImplantByIdQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting implant with id: {@Id}", request.Id);

		var implant = await implantRepository.FindByIdAsync(request.Id);
		if (implant == null) 
		{
			throw new NotFoundException(nameof(implant), request.Id.ToString());
		}

		var implantDto = mapper.Map<ImplantDto>(implant);
		return implantDto;
	}
}
