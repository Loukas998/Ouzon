using Template.Application.Kits.Dtos;
using Template.Application.Tools.Dtos;
using Template.Application.Users.Dtos;
using Template.Domain.Enums;

namespace Template.Application.Procedure.Dtos.MainProcedure;

public class ProcedureDto
{
    public int Id { get; set; }
    public string DoctorId { get; set; }
    public int NumberOfAssistants { get; set; }
    public List<string>? AssistantIds { get; set; }
    public int CategoryId { get; set; }
    public EnumProcedureStatus Status { get; set; } = EnumProcedureStatus.REQUEST_SENT;
    public DateTime Date { get; set; }
    public UserDto Doctor { get; set; }
    public IEnumerable<ToolDto>? Tools { get; set; } = [];
    public IEnumerable<KitDto>? Kits { get; set; } = [];
    public IEnumerable<UserDto>? Assistants { get; set; } = [];

}
