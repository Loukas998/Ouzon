using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Tools.Commands.Create;
using Template.Application.Tools.Commands.Delete;
using Template.Application.Tools.Commands.Update;
using Template.Application.Tools.Dtos;
using Template.Application.Tools.Queries;
using Template.Application.Tools.Queries.GetAll;
using Template.Application.Tools.Queries.GetById;
using Template.Application.Tools.Queries.GetWithFilter;

namespace Template.API.Controllers
{
    [ApiController]
    [Route("api/tools")]
    public class ToolController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateTool([FromBody] CreateToolCommand request)
        {
            var res= await mediator.Send(request);
            if (!res.SuccessStatus)
            {
                return BadRequest(res.Errors);

            }
            int Id = res.Data;
            return CreatedAtAction(nameof(GetToolById), new{ Id },null);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ToolDto>> GetToolById([FromRoute]int Id)
        {
            var res = await mediator.Send(new GetToolByIdQuery(Id));
            if (!res.SuccessStatus)
            {
                return BadRequest(res.Errors);
            }
            return Ok();
        }
        [HttpPatch]
        public async Task<ActionResult> UpdateTool(UpdateToolCommand request)
        {
           var res =  await mediator.Send(request);
            if (!res.SuccessStatus)
            {
                return BadRequest(res.Errors);
            }
            return NoContent();
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteTool([FromRoute] int Id)
        {
            var command = new DeleteToolCommand() { Id = Id };
           var res = await mediator.Send(command);
            if (!res.SuccessStatus)
            {
                return BadRequest(res.Errors);
            }
            return NoContent();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToolDto>>> GetAllTools()
        {
            var res = await mediator.Send(new GetAllToolsQuery());
            if (!res.SuccessStatus)
            {
                return BadRequest(res.Errors);
            }
            if (!res.Data.Any())
            {
                return NotFound();
            }
            return Ok(res.Data);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ToolDto>>> GetFilteredTools([FromQuery] GetToolsWithFilterQuery query)
        {
            return Ok(await mediator.Send(query));
        }
    }
}
