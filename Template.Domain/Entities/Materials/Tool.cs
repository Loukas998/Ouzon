using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Template.Domain.Entities.ProcedureRelatedEntities;

namespace Template.Domain.Entities.Materials;

public class Tool : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Thickness { get; set; }
    public int Quantity { get; set; }
    public int? KitId { get; set; }
    public int? CategoryId { get; set; }
    public string? ImagePath { get; set; }

    public Kit? Kit { get; set; }
    public Category? Category { get; set; }
    [JsonIgnore]
    public List<ProcedureTool>? ProceduresWithTool { get; set; }
    public List<ProcedureImplantTool>? ProcedureImplantTools { get; set; }

}
