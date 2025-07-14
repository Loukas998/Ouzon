using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.Entities.ProcedureRelatedEntities;

public class ProcedureAssistant
{
    public int Id { get; set; }
    public string AsisstantId { get; set; }
    public int ProcedureId { get; set; }
    public User Asisstant { get; set; }
    public Procedure Procedure{ get; set; }

}
