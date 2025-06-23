using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Procedure.Commands.Create;
using Template.Application.Procedure.Queries.GetById;

namespace Template.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProceduresController (IMediator mediator):ControllerBase
{
    [HttpPost(Name ="AddProcedure")]
    public async Task<IActionResult>AddProcedure(CreateProcedureCommand request)
    {
        var id = await mediator.Send(request);
        return CreatedAtAction(nameof(GetProcedure), new { id }, null);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProcedure([FromRoute]int id)
    {
        var result = await mediator.Send(new GetProcedureQuery(id));
        return Ok(result);
    }
}
