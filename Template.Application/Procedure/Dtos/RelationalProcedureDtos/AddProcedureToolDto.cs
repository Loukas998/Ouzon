using System.ComponentModel.DataAnnotations;

namespace Template.Application.Procedure.Dtos
{
    public class AddProcedureToolDto
    {
        public int ToolId { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
