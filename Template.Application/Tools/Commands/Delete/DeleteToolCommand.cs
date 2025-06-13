using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.Tools.Commands.Delete
{
   public class DeleteToolCommand :IRequest
    {
        public int Id { get; set; }
    }
}
