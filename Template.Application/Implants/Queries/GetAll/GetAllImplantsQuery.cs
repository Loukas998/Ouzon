using MediatR;
using Template.Application.Implants.Dtos;

namespace Template.Application.Implants.Queries.GetAll;

public class GetAllImplantsQuery : IRequest<IEnumerable<ImplantDto>>
{
}
