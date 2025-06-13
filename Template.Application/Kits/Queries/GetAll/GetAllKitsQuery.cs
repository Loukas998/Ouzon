using MediatR;
using Template.Application.Kits.Dtos;

namespace Template.Application.Kits.Queries.GetAll;

public class GetAllKitsQuery : IRequest<IEnumerable<KitDto>>
{
}
