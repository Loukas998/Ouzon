namespace Template.Domain.Entities.ProcedureRelatedEntities;

public class ProcedureAssistant : BaseEntity
{
    public int Id { get; set; }
    public string AsisstantId { get; set; }
    public int ProcedureId { get; set; }
    public User Asisstant { get; set; }
    public Procedure Procedure { get; set; }

}
