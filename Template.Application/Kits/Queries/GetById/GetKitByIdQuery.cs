using MediatR;
using Template.Application.Kits.Dtos;

namespace Template.Application.Kits.Queries.GetById;

public class GetKitByIdQuery(int id) : IRequest<KitDto>
{
	public int Id { get; } = id;
}
