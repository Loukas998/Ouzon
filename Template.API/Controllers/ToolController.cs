using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Tools.Commands.Create;
using Template.Application.Tools.Commands.Delete;
using Template.Application.Tools.Commands.Update;
using Template.Application.Tools.Queries;
using Template.Application.Tools.Queries.GetAll;
using Template.Application.Tools.Queries.GetById;

namespace Template.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToolController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateTool([FromBody] CreateToolCommand request)
        {
            var Id = await mediator.Send(request);
            return CreatedAtAction(nameof(GetToolById), new{ Id },null);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetToolById([FromRoute]int Id)
        {
            return Ok(await mediator.Send(new GetToolByIdQuery(Id)));
        }
        [HttpPatch]
        public async Task<ActionResult> UpdateTool(UpdateToolCommand request)
        {
            await mediator.Send(request);
            return NoContent();
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteTool([FromRoute] int Id)
        {
            var command = new DeleteToolCommand() { Id = Id };
            await mediator.Send(command);
            return NoContent();
        }
        [HttpGet]
        public async Task<ActionResult> GetAllTools()
        {
            var tools = await mediator.Send(new GetAllToolsQuery());
            if (!tools.Any())
            {
                return NoContent();
            }
            return Ok(tools);
        }
    }
}
