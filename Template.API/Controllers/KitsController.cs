using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Kits.Commands.Create;
using Template.Application.Kits.Commands.Delete;
using Template.Application.Kits.Dtos;
using Template.Application.Kits.Queries.GetAll;
using Template.Application.Kits.Queries.GetById;

namespace Template.API.Controllers;

[ApiController]
[Route("api/kits")]
public class KitsController(IMediator mediator) : ControllerBase
{
	[HttpPost]
	public async Task<IActionResult> CreateKit([FromBody] CreateKitCommand command)
	{
		int id = await mediator.Send(command);
		return CreatedAtAction(nameof(GetKitById), new { id }, null);
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<KitDto>>> GetAllKits()
	{
		var kits = await mediator.Send(new GetAllKitsQuery());
		return Ok(kits);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<KitDto>> GetKitById([FromRoute] int id)
	{
		var kit = await mediator.Send(new GetKitByIdQuery(id));
		return Ok(kit);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteKit([FromRoute] int id)
	{
		await mediator.Send(new DeleteKitCommand(id));
		return NoContent();
	}
}
