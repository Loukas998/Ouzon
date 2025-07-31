using MediatR;
using Template.Application.Users.Dtos;

namespace Template.Application.Users.Queries.GetAllAssistants;

public class GetAllAssistantsQuery(string? sortByRating) : IRequest<IEnumerable<UserDto>>
{
    public string? SortByRating { get; } = sortByRating;
}
