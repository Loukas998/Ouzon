using MediatR;
using Template.Application.Ratings.Dtos;

namespace Template.Application.Ratings.Queries.CurrentAssistantRatings;

public class GetCurrentAssistantRatingsQuery : IRequest<IEnumerable<RatingsDto>>
{
}
