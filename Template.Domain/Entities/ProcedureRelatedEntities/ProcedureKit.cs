using Template.Domain.Entities.Materials;

namespace Template.Domain.Entities.ProcedureRelatedEntities
{
    public class ProcedureKit : BaseEntity
    {
        public int Id { get; set; }
        public int KitId { get; set; }
        public int ProcedureId { get; set; }
        public Procedure? Procedure { get; set; }
        public Kit? Kit { get; set; }
    }
}
