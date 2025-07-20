using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Implants.Commands.Create;
using Template.Application.Implants.Commands.Delete;
using Template.Application.Implants.Commands.Update;
using Template.Application.Implants.Dtos;
using Template.Application.Implants.Queries.GetAll;
using Template.Application.Implants.Queries.GetById;
using Template.Application.Implants.Queries.GetWithFilter;

namespace Template.API.Controllers;

[ApiController]
[Route("api/implants")]
public class ImplantsController(IMediator mediator) : ControllerBase
{
	[HttpPost]
	public async Task<IActionResult> CreateImplant([FromBody] CreateImplantCommand command)
	{
		var res = await mediator.Send(command);
		if (!res.SuccessStatus)
		{
			return BadRequest(res.Errors);
		}
		var id = res.Data;
		return CreatedAtAction(nameof(GetImplantById), new { id }, null);
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<ImplantDto>>> GetAllImplants()
	{
		var implants = await mediator.Send(new GetAllImplantsQuery());
		if (!implants.SuccessStatus)
		{
			return NotFound(implants.Errors);
		}
		return Ok(implants.Data);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ImplantDto>> GetImplantById([FromRoute] int id)
	{
		var implant = await mediator.Send(new GetImplantByIdQuery(id));
		if (!implant.SuccessStatus)
		{
			return NotFound(implant.Errors);
		}
		return Ok(implant.Data);
	}
	//[HttpGet("filter")]
 //   public async Task<ActionResult<ImplantDto>> GetImplantsWithFilters([FromQuery])
 //   {
 //       var implant = await mediator.Send(new GetImplantByIdQuery(id));
 //       if (!implant.SuccessStatus)
 //       {
 //           return NotFound(implant.Errors);
 //       }
 //       return Ok(implant.Data);
 //   }

    [HttpDelete("{id}")]
	public async Task<IActionResult> DeleteImplant([FromRoute] int id)
	{
		var res = await mediator.Send(new DeleteImplantCommand(id));
		if (!res.SuccessStatus)
		{
			return BadRequest(res.Errors);
		}
		return NoContent();
	}

	[HttpPatch]
	[Route("{implantId:int}")]
	public async Task<IActionResult> UpdateImplant([FromRoute] int implantId, [FromBody] UpdateImplantCommand command)
	{
		command.ImplantId = implantId;
		var res= await mediator.Send(command);
		if (!res.SuccessStatus)
		{
			return BadRequest(res.Errors);
		}
		return NoContent();
	}

	[HttpGet("GetFilteredImplants")]
	public async Task<ActionResult<IEnumerable<ImplantDto>>> GetFilteredImplants([FromQuery] GetImplantsWithFilterQuery query)
	{
		return Ok(await mediator.Send(query));
	}
}
