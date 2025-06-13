using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Tools.Dtos;

namespace Template.Application.Tools.Queries.GetById
{
    public class GetToolByIdQuery(int Id) : IRequest<ToolDto>
    {
        public int Id { get; set; } = Id;
    }
}
