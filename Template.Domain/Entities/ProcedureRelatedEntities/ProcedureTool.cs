using Template.Domain.Entities.Materials;

namespace Template.Domain.Entities.ProcedureRelatedEntities
{
    public class ProcedureTool : BaseEntity
    {
        public int Id { get; set; }
        public int ToolId { get; set; }
        public int ProcedureId { get; set; }
        public Procedure? Procedure { get; set; }
        public Tool? Tool { get; set; }
    }
}
