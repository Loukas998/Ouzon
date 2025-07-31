using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Template.Application.Ratings.Commands.Rate;

public class AddRatingToAssistantCommand : IRequest
{
    public string? Note { get; set; }
    [Range(0, 5)]
    public int Rate { get; set; }
    public string? AssistantId { get; set; }
}
