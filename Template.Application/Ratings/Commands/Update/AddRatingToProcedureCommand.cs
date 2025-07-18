using Template.Application.Abstraction.Commands;

namespace Template.Application.Ratings.Commands.Update;

public class AddRatingToProcedureCommand:ICommand<int>
{
    public int RatingValue { get; set; }
    public string? Comment { get; set; }
    public int ProcedureId { get; set; }
}
