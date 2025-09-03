using Template.Domain.Entities.Materials;

namespace Template.Domain.Entities.ProcedureRelatedEntities
{
    public class ProcedureImplant : BaseEntity
    {
        public int Id { get; set; }
        public int ProcedureId { get; set; }
        public int ImplantId { get; set; }
        public Procedure Procedure { get; set; }
        public Implant Implant { get; set; }
    }
}
