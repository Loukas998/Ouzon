using AutoMapper;
using MediatR;
using Template.Application.Users.Dtos;
using Template.Domain.Repositories;

namespace Template.Application.Users.Queries.GetAllAssistants
{
    public class GetAllAssistantsQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        : IRequestHandler<GetAllAssistantsQuery, IEnumerable<UserDto>>
    {
        public async Task<IEnumerable<UserDto>> Handle(GetAllAssistantsQuery request, CancellationToken cancellationToken)
        {
            var users = await accountRepository.GetAssistants(request.SortByRating);
            var result = mapper.Map<IEnumerable<UserDto>>(users);
            return result;
        }
    }
}
