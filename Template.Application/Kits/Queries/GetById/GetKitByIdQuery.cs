using MediatR;
using Template.Application.Abstraction.Queries;
using Template.Application.Kits.Dtos;

namespace Template.Application.Kits.Queries.GetById;

public class GetKitByIdQuery(int id) : IQuery<KitDto>
{
	public int Id { get; } = id;
}
