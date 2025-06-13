using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Template.Domain.Entities.Materials;
using Template.Domain.Repositories;

namespace Template.Application.Kits.Commands.Create;

public class CreateKitCommandHandler(IKitRepository kitRepository, IMapper mapper, 
	ILogger<CreateKitCommandHandler> logger) : IRequestHandler<CreateKitCommand, int>
{
	public async Task<int> Handle(CreateKitCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Creating kit: {@Kit}", request);
		var kit = mapper.Map<Kit>(request);

		var result = await kitRepository.AddAsync(kit);
		return result.Id;
	}
}
