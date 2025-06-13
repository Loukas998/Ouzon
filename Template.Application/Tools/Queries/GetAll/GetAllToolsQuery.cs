using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Tools.Dtos;

namespace Template.Application.Tools.Queries.GetAll
{
   public class GetAllToolsQuery :IRequest<IEnumerable<ToolDto>>
    {
    }
}
