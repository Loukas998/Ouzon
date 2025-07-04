using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Holidays.Commands.ChangeStatus;
using Template.Application.Holidays.Commands.Create;
using Template.Application.Holidays.Commands.Delete;
using Template.Application.Holidays.Dtos;
using Template.Application.Holidays.Queries.GetAll;
using Template.Application.Holidays.Queries.GetById;
using Template.Application.Implants.Commands.Delete;
using Template.Application.Implants.Dtos;
using Template.Application.Implants.Queries.GetAll;

namespace Template.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class HolidaysController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateHoliday([FromBody] CreateHolidayCommand command)
    {
        int id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetHolidayById), new { id }, null);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HolidayDto>> GetHolidayById([FromRoute] int id)
    {
        var holiday = await mediator.Send(new GetHolidayByIdQuery(id));
        return Ok(holiday);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HolidayDto>>> GetAllHolidays()
    {
        var holidays = await mediator.Send(new GetAllHolidaysQuery());
        return Ok(holidays);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteHoliday([FromRoute] int id)
    {
        await mediator.Send(new DeleteHolidayCommand(id));
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] ChangeHolidayStatusCommand command)
    {
        command.HolidayId = id;
        await mediator.Send(command);
        return NoContent();
    }
}
