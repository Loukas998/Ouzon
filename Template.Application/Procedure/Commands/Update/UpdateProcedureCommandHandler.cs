using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Repositories;

namespace Template.Application.Procedure.Commands.Update
{
    public class UpdateProcedureCommandHandler (IMapper mapper,IProcedureRepository procedureRepository): IRequestHandler<UpdateProcedureCommand>
    {
        public async Task Handle(UpdateProcedureCommand request, CancellationToken cancellationToken)
        {
            var procedure = await procedureRepository.FindByIdAsync(request.Id);
            if(procedure == null)
            {
                throw new Exception();
            }
            mapper.Map(request, procedure);
            await procedureRepository.UpdateAsync(procedure);
            return;
        }
    }
}
