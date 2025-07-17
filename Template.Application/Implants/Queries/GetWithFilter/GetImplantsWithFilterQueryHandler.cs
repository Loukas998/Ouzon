using AutoMapper;
using MediatR;
using System.Collections.Immutable;
using Template.Application.Implants.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Implants.Queries.GetWithFilter;

public class GetImplantsWithFilterQueryHandler(IImplantRepository implantRepository, IMapper mapper)
    : IRequestHandler<GetImplantsWithFilterQuery, IEnumerable<ImplantDto>>
{
    public async Task<IEnumerable<ImplantDto>> Handle(GetImplantsWithFilterQuery request, CancellationToken cancellationToken)
    {
        var implants = await implantRepository.GetFilteredImplants(
            request.Brand,
            request.Radius,
            request.Width,
            request.Height,
            request.KitId,
            request.PageNum,
            request.PageSize);

        var result = mapper.Map<IEnumerable<ImplantDto>>(implants);
        return result;
    }
}
