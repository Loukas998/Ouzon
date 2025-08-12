using AutoMapper;
using MediatR;
using Template.Application.Ratings.Dtos;
using Template.Application.Users;
using Template.Domain.Repositories;

namespace Template.Application.Ratings.Queries.CurrentAssistantRatings;

public class GetCurrentAssistantRatingsQueryHandler(IRatingsRepository ratingsRepository, IUserContext userContext,
    IAccountRepository accountRepository, IMapper mapper)
    : IRequestHandler<GetCurrentAssistantRatingsQuery, IEnumerable<RatingsDto>>
{
    public async Task<IEnumerable<RatingsDto>> Handle(GetCurrentAssistantRatingsQuery request, CancellationToken cancellationToken)
    {
        var currentAssistant = userContext.GetCurrentUser();
        var assistant = await accountRepository.GetUserAsync(currentAssistant.Id, currentAssistant.Roles.Contains("AssistantDoctor"));

        var assistantRatings = assistant.RatingsReceived.OrderByDescending(r => r.Id).ToList();
        var results = mapper.Map<IEnumerable<RatingsDto>>(assistantRatings);

        foreach (var result in results)
        {
            var doctor = await accountRepository.GetUserAsync(result.DoctorId, false);
            result.DoctorName = doctor.UserName;
        }

        return results;
    }
}
