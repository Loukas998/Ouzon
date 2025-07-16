using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Application.Procedure.Dtos
{
  public  class AddProcedureToolDto
    {
        public int ToolId { get; set; }
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
    }
}
