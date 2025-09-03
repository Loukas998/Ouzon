using System.ComponentModel.DataAnnotations;
using Template.Domain.Entities.ProcedureRelatedEntities;

namespace Template.Domain.Entities.Materials;

public class Category : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }

    public List<Tool> Tools { get; set; }
    public List<Procedure> Procedures { get; set; }
}
