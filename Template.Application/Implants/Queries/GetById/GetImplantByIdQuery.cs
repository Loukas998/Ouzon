using MediatR;
using Template.Application.Abstraction.Queries;
using Template.Application.Implants.Dtos;

namespace Template.Application.Implants.Queries.GetById;

public class GetImplantByIdQuery(int id) : IQuery<ImplantDto>
{
	public int Id { get; } = id;
}
