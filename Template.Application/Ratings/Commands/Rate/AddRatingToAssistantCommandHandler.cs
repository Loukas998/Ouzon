using AutoMapper;
using MediatR;
using Template.Application.Users;
using Template.Domain.Entities.Users;
using Template.Domain.Repositories;

namespace Template.Application.Ratings.Commands.Rate;

public class AddRatingToAssistantCommandHandler(IRatingsRepository ratingsRepository, IMapper mapper,
    IUserContext userContext) : IRequestHandler<AddRatingToAssistantCommand>
{
    public async Task Handle(AddRatingToAssistantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var rating = mapper.Map<Rating>(request);
            rating.DoctorId = userContext.GetCurrentUser()!.Id;
            await ratingsRepository.AddAsync(rating);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }
}
