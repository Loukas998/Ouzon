using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Kits.Commands.Create;
using Template.Application.Kits.Commands.Delete;
using Template.Application.Kits.Dtos;
using Template.Application.Kits.Queries.GetAll;
using Template.Application.Kits.Queries.GetById;
using Template.Domain.Entities.Materials;

namespace Template.API.Controllers;

[ApiController]
[Route("api/kits")]
public class KitsController(IMediator mediator) : ControllerBase
{
	[HttpPost]
	public async Task<IActionResult> CreateKit([FromBody] CreateKitCommand command)
	{
		var res = await mediator.Send(command);
		if (!res.SuccessStatus)
		{
			return BadRequest(res.Errors);

		}
		int id = res.Data;
		return CreatedAtAction(nameof(GetKitById), new { id }, null);
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<KitDto>>> GetAllKits()
	{
		var kits = await mediator.Send(new GetAllKitsQuery());
		if (!kits.SuccessStatus)
		{
			return BadRequest(kits.Errors);
		}
        if (!kits.Data.Any())
		{
			return NotFound();
		}
		return Ok(kits.Data);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<KitDto>> GetKitById([FromRoute] int id)
	{
		var kit = await mediator.Send(new GetKitByIdQuery(id));
        if (!kit.SuccessStatus)
		{
			return BadRequest(kit.Errors);
		}
		return Ok(kit);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteKit([FromRoute] int id)
	{
		var res =  await mediator.Send(new DeleteKitCommand(id));
        if (!res.SuccessStatus)
        {
            return BadRequest(res.Errors);
        }
        return NoContent();
	}
}
