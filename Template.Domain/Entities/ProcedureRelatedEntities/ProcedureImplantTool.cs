using Template.Domain.Entities.Materials;

namespace Template.Domain.Entities.ProcedureRelatedEntities
{
    public class ProcedureImplantTool : BaseEntity
    {
        public int Id { get; set; }
        public int ProcedureId { get; set; }
        public int ImplantId { get; set; }
        public int ToolId { get; set; }
        public Procedure Procedure { get; set; }
        public Tool Tool { get; set; }
        public Implant Implant { get; set; }
    }
}
