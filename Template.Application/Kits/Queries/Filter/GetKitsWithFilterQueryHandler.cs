using AutoMapper;
using Template.Application.Abstraction.Queries;
using Template.Application.Kits.Dtos;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Repositories;

namespace Template.Application.Kits.Queries.Filter
{
    public class GetKitsWithFilterQueryHandler(IMapper mapper, IKitRepository kitRepository) : IQueryHandler<GetKitsWithFilterQuery, List<KitDto>>
    {
        public async Task<Result<List<KitDto>>> Handle(GetKitsWithFilterQuery request, CancellationToken cancellationToken)
        {
            var kits = await kitRepository.GetFilteredKit(request.PageNum, request.PageSize, request.BrandName, request.HasImplants, request.IsMainKit);
            if (kits.Count == 0)
            {
                return Result.Failure<List<KitDto>>(["Kits not Found"]);
            }
            var res = mapper.Map<List<KitDto>>(kits);
            return Result.Success(res);
        }
    }
}
