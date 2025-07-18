using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Commands;
using Template.Application.Users;
using Template.Domain.Entities.ResponseEntity;
using Template.Domain.Entities.Users;
using Template.Domain.Repositories;

namespace Template.Application.Ratings.Commands.Update
{
    public class AddRatingToProcedureCommandHandler(IUserContext userContext,IProcedureRepository procedureRepository,
        IRatingsRepository ratingsRepository,IMapper mapper) : ICommandHandler<AddRatingToProcedureCommand,int>
    {
        public async Task<Result<int>> Handle(AddRatingToProcedureCommand request, CancellationToken cancellationToken)
        {
            var procedure = await procedureRepository.FindByIdAsync(request.ProcedureId);
            if(procedure == null)
            {
                return Result.Failure<int>(["Data Doesn't Exist"]);
            }
            var currentUser = userContext.GetCurrentUser();
            if(currentUser == null ||procedure.DoctorId != currentUser.Id)
            {
                return Result.Failure<int>(["This User isn't the Doctor of This Procedure"]);
            }
            var rating = mapper.Map<Rating>(request);
            var result = await ratingsRepository.AddAsync(rating);
            return Result.Success(result.Id);
        }
    }
}
