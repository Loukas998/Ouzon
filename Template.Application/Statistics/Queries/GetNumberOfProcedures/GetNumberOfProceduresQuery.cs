using MediatR;

namespace Template.Application.Statistics.Queries.GetNumberOfProcedures;

public class GetNumberOfProceduresQuery : IRequest<int>
{
    public int? Month { get; set; }
    public int? Year { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
