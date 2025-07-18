using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Queries;
using Template.Application.Users.Dtos;

namespace Template.Application.Procedure.Queries.GetWithAssistants
{
    public class GetProcedureAssistantsQuery(int Id) : IQuery<List<UserDto>>
    {
        public int Id { get; set; } = Id;
    }
}
