using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Holidays.Commands.ChangeStatus;
using Template.Application.Holidays.Commands.Create;
using Template.Application.Holidays.Commands.Delete;
using Template.Application.Holidays.Dtos;
using Template.Application.Holidays.Queries.GetAll;
using Template.Application.Holidays.Queries.GetById;
using Template.Application.Holidays.Queries.GetWithFilter;

namespace Template.API.Controllers;

[ApiController]
[Route("api/holidays")]
public class HolidaysController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateHoliday([FromBody] CreateHolidayCommand command)
    {
        var result = await mediator.Send(command);
        int id = result.Data;
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return CreatedAtAction(nameof(GetHolidayById), new { id }, null);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HolidayDto>> GetHolidayById([FromRoute] int id)
    {
        var holiday = await mediator.Send(new GetHolidayByIdQuery(id));
        if (!holiday.SuccessStatus)
        {
            return NotFound(holiday.Errors);
        }
        return Ok(holiday.Data);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HolidayDto>>> GetAllHolidays()
    {
        var holidays = await mediator.Send(new GetAllHolidaysQuery());
        if (!holidays.SuccessStatus)
        {
            return BadRequest(holidays.Errors);
        }
        if (!holidays.Data.Any())
        {
            return NotFound();
        }
        return Ok(holidays.Data);
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<HolidayDto>>> FilterHolidays([FromQuery] int pageNum, int pageSize, DateTime? FromDate, DateTime? ToDate, string? AssistantId)
    {
        var holidays = await mediator.Send(new GetHolidayWithFilterQuery(pageNum, pageSize, FromDate, ToDate, AssistantId));
        if (!holidays.SuccessStatus)
        {
            return BadRequest(holidays.Errors);
        }
        if (!holidays.Data.Any())
        {
            return NotFound();
        }
        return Ok(holidays.Data);
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteHoliday([FromRoute] int id)
    {
        var res = await mediator.Send(new DeleteHolidayCommand(id));
        if (!res.SuccessStatus)
        {
            return BadRequest(res.Errors);
        }
        return NoContent();
    }

    [HttpPut("ChangeStatus")]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeHolidayStatusCommand command)
    {
        var result = await mediator.Send(command);
        if (!result.SuccessStatus)
        {
            return BadRequest(result.Errors);
        }
        return NoContent();
    }
}
