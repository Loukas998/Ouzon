using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities.Materials;
using Template.Domain.Enums;

namespace Template.Domain.Entities.ProcedureRelatedEntities;

public class Procedure
{
    public int Id { get; set; }
    public string DoctorId { get; set; }
    public int NumberOfAsisstants { get; set; }
    public int CategoryId { get; set; }
    public EnumProcedureStatus Status { get; set; }
    public DateTime Date { get; set; }
    public List<ProcedureKit> KitsInProcedure { get; set; } = [];
    public List<ProcedureTool> ToolsInProcedure { get; set; } = [];
    public User Doctor { get; set; }
    public List<ProcedureAssistant>? AssistantsInProcedure { get; set; }
    public Category Category { get; set; }
}
