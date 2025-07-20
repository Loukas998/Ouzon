using AutoMapper;
using Microsoft.Extensions.Logging;
using Template.Application.Abstraction.Queries;
using Template.Application.Kits.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Kits.Queries.GetAll;

public class GetAllKitsQueryHandler(IKitRepository kitRepository, IMapper mapper,
    ILogger<GetAllKitsQueryHandler> logger) : IQueryHandler<GetAllKitsQuery, IEnumerable<KitDto>>
{
    public async Task<Result<IEnumerable<KitDto>>> Handle(GetAllKitsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all kits");

        var kits = await kitRepository.GetKitsWithToolsAndImplantsCount();

        var kitsDtos = kits.Select(ki => new KitDto()
        {
            Id = ki.Id,
            Name = ki.Name,
            ImplantCount = ki.ImplantCount,
            ToolCount = ki.ToolCount
        });
        return Result.Success(kitsDtos);
    }
}
