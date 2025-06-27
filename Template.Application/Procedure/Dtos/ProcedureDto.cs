using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application.Kits.Dtos;
using Template.Application.Tools.Dtos;
using Template.Domain.Entities.Materials;
using Template.Domain.Enums;

namespace Template.Application.Procedure.Dtos;

public  class ProcedureDto
{
    public int Id { get; set; }
    public string DoctorId { get; set; }
    public string? AssistantId { get; set; }
    public int CategoryId { get; set; }
    public EnumProcedureStatus Status { get; set; } = EnumProcedureStatus.REQUEST_SENT;
    public DateTime Date { get; set; }
    public List<ToolDto>? Tools { get; set; } = [];
    public List<KitDto>? Kits{ get; set; } = [];
    
}
