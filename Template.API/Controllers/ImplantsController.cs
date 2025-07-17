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
[Route("api/[controller]")]
public class ImplantsController(IMediator mediator) : ControllerBase
{
	[HttpPost]
	public async Task<IActionResult> CreateImplant([FromBody] CreateImplantCommand command)
	{
		int id = await mediator.Send(command);
		return CreatedAtAction(nameof(GetImplantById), new { id }, null);
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<ImplantDto>>> GetAllImplants()
	{
		var implants = await mediator.Send(new GetAllImplantsQuery());
		return Ok(implants);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ImplantDto>> GetImplantById([FromRoute] int id)
	{
		var implant = await mediator.Send(new GetImplantByIdQuery(id));
		return Ok(implant);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteImplant([FromRoute] int id)
	{
		await mediator.Send(new DeleteImplantCommand(id));
		return NoContent();
	}

	[HttpPatch]
	[Route("{implantId:int}")]
	public async Task<IActionResult> UpdateImplant([FromRoute] int implantId, [FromBody] UpdateImplantCommand command)
	{
		command.ImplantId = implantId;
		await mediator.Send(command);
		return NoContent();
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<ImplantDto>>> GetFilteredImplants([FromQuery] GetImplantsWithFilterQuery query)
	{
		return Ok(await mediator.Send(query));
	}
}
