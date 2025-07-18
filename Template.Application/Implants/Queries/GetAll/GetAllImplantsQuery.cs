using MediatR;
using Template.Application.Abstraction.Queries;
using Template.Application.Implants.Dtos;

namespace Template.Application.Implants.Queries.GetAll;

public class GetAllImplantsQuery : IQuery<IEnumerable<ImplantDto>>
{
}
