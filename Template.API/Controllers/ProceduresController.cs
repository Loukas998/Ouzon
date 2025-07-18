using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Procedure.Commands.AssignAssistnatsToProcedure;
using Template.Application.Procedure.Commands.Create;
using Template.Application.Procedure.Commands.Update;
using Template.Application.Procedure.Dtos;
using Template.Application.Procedure.Queries.GetAll;
using Template.Application.Procedure.Queries.GetById;
using Template.Application.Procedure.Queries.GetPaged;
using Template.Application.Procedure.Queries.GetWithAssistants;
using Template.Application.Procedure.Queries.GetWithKitsOnly;

namespace Template.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProceduresController (IMediator mediator):ControllerBase
{
    [HttpPost(Name ="AddProcedure")]
    public async Task<IActionResult>AddProcedure(CreateProcedureCommand request)
    {
        var result = await mediator.Send(request);
        var id = result.Data;
        return CreatedAtAction(nameof(GetProcedure), new { id }, null);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ProcedureDto>> GetProcedure([FromRoute]int id)
    {
        var result = await mediator.Send(new GetProcedureQuery(id));
       
        return Ok(result.Data);
    }
    [HttpGet(Name = "GetAllProcedures")]
    public async Task<ActionResult<IEnumerable<ProcedureDto>>> GetAllProcedures()
    {
        var result = await mediator.Send(new GetAllProceduresQuery());
        if (!result.Data.Any())
        {
            return NotFound();
        }
        return Ok(result.Data);
    }
    [HttpGet(Name = "GetProceduresPaged")]
    public async Task<ActionResult<IEnumerable<ProcedureDto>>>GetProceduresPaged([FromQuery]int pageSize,int pageNum,string?DoctorId,string?AssistantId)
    {
        var result = await mediator.Send(new GetProceduresPagedQuery(pageSize,pageNum,DoctorId,AssistantId));
        if (!result.Data.Any())
        {
            return NotFound();
        }
        return Ok(result.Data);
    }
    [HttpPut(Name = "UpdateProcedure")]
    public async Task<IActionResult>UpdateProcedure(UpdateProcedureCommand request)
    {
        var result = await mediator.Send(request);
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return NoContent();
    }
    [HttpPut(Name = "AssignAssistantToProcedure")]
    public async Task<IActionResult> AssignAssistantToProcedure(AssignAssistantsToProcedureCommand request)
    {
        var result = await mediator.Send(request);
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return NoContent();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ProcedureKitDetailsDto>>GetProcedureKits(int id)
    {
        var result = await mediator.Send(new GetProcedureWithKitsOnlyQuery(id));
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ProcedureKitDetailsDto>> GetProcedureAssistants(int id)
    {
        var result = await mediator.Send(new GetProcedureAssistantsQuery(id));
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }
}
