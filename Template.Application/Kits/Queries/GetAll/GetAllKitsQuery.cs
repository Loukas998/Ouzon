using MediatR;
using Template.Application.Abstraction.Queries;
using Template.Application.Kits.Dtos;

namespace Template.Application.Kits.Queries.GetAll;

public class GetAllKitsQuery : IQuery<IEnumerable<KitDto>>
{
}
