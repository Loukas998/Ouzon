using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Abstraction.Commands;


namespace Template.Application.Tools.Commands.Delete
{
   public class DeleteToolCommand :ICommand
    {
        public int Id { get; set; }
    }
}
