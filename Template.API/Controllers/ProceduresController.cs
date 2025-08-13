using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Procedure.Commands.AssignAssistnatsToProcedure;
using Template.Application.Procedure.Commands.ChangeStatus;
using Template.Application.Procedure.Commands.Create;
using Template.Application.Procedure.Commands.Update;
using Template.Application.Procedure.Dtos;
using Template.Application.Procedure.Dtos.MainProcedure;
using Template.Application.Procedure.Queries.GetAll;
using Template.Application.Procedure.Queries.GetById;
using Template.Application.Procedure.Queries.GetPaged;
using Template.Application.Procedure.Queries.GetWithAssistants;
using Template.Application.Procedure.Queries.GetWithKitsOnly;
using Template.Domain.Enums;

namespace Template.API.Controllers;

[ApiController]
[Route("api/procedures")]
public class ProceduresController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddProcedure(CreateProcedureCommand request)
    {
        var result = await mediator.Send(request);
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        var id = result.Data;
        return CreatedAtAction(nameof(GetProcedure), new { id }, null);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ProcedureDetailedDto>> GetProcedure([FromRoute] int id)
    {
        var result = await mediator.Send(new GetProcedureQuery(id));
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpPost("FilteredProcedure")]
    public async Task<ActionResult<IEnumerable<ProcedureDto>>> GetAllProcedures([FromQuery] DateTime? from, DateTime? to, int? minNumberOfAssistants, int? maxNumberOfAssistants, string? doctorName,
        List<string>? assistantNames, string? clinicName, string? clinicAddress, EnumProcedureStatus? status)
    {
        var result = await mediator.Send(
            new GetAllProceduresQuery(
                from, to, minNumberOfAssistants, maxNumberOfAssistants,
                doctorName, assistantNames, clinicName, clinicAddress, status));

        if (!result.SuccessStatus)
        {
            return NotFound(result.Errors);
        }
        if (!result.Data.Any())
        {
            return NotFound();
        }
        return Ok(result.Data);
    }

    [HttpGet("paged")]
    public async Task<ActionResult<IEnumerable<ProcedureDto>>> GetProceduresPaged([FromQuery] int pageSize, int pageNum, string? DoctorId, string? AssistantId)
    {
        var result = await mediator.Send(new GetProceduresPagedQuery(pageSize, pageNum, DoctorId, AssistantId));
        if (!result.Data.Any())
        {
            return NotFound();
        }
        return Ok(result.Data);
    }
    [HttpPatch]
    public async Task<IActionResult> UpdateProcedure(UpdateProcedureCommand request)
    {
        var result = await mediator.Send(request);
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return NoContent();
    }
    [HttpPut]
    public async Task<IActionResult> AssignAssistantToProcedure(AssignAssistantsToProcedureCommand request)
    {
        var result = await mediator.Send(request);
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return NoContent();
    }
    [HttpGet("{id}/kits")]
    public async Task<ActionResult<ProcedureKitDetailsDto>> GetProcedureKits(int id)
    {
        var result = await mediator.Send(new GetProcedureWithKitsOnlyQuery(id));
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }
    [HttpGet("{id}/assistants")]
    public async Task<ActionResult<ProcedureKitDetailsDto>> GetProcedureAssistants(int id)
    {
        var result = await mediator.Send(new GetProcedureAssistantsQuery(id));
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result.Data);
    }

    [HttpPatch("{id}/ChangeStatus")]
    public async Task<ActionResult<ProcedureDetailedDto>> ChangeProcedureStatus([FromBody] ChangeStatusCommand command)
    {
        return await mediator.Send(command);
    }
}
