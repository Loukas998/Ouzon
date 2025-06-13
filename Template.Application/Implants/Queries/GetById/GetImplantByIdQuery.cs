using MediatR;
using Template.Application.Implants.Dtos;

namespace Template.Application.Implants.Queries.GetById;

public class GetImplantByIdQuery(int id) : IRequest<ImplantDto>
{
	public int Id { get; } = id;
}
