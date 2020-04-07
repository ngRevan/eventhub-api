using EventHub.Service.Commands.Messaging.Events;
using EventHub.Service.Queries.Messaging.Events;
using EventHub.Service.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventHub.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ResponseCache(CacheProfileName = "Never")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "getEvents")]
        public async Task<ActionResult<IEnumerable<EventView>>> Get([FromQuery]EventListQuery message)
        {
            var result = await _mediator.Send(message);

            return Ok(result.Results);
        }

        [HttpGet("{eventId:guid}", Name = "getEventById")]
        public async Task<ActionResult<EventView>> Get(Guid eventId)
        {
            var result = await _mediator.Send(new EventDetailQuery { Id = eventId });

            return Ok(result.Result);
        }

        [HttpPost(Name = "createEvent")]
        public async Task<IActionResult> Post([FromBody]CreateEventCommand message)
        {
            await _mediator.Send(message);

            return CreatedAtAction(nameof(Get), new { @eventId = message.Id }, null);
        }

        [HttpPut("{eventId:guid}", Name = "updateEvent")]
        public async Task<IActionResult> Put(Guid eventId, [FromBody]UpdateEventCommand message)
        {
            message.Id = eventId;
            await _mediator.Send(message);

            return NoContent();
        }

        [HttpDelete("{eventId:guid}", Name = "deleteEvent")]
        public async Task<IActionResult> Delete(Guid eventId)
        {
            await _mediator.Send(new DeleteEventCommand { Id = eventId });

            return NoContent();
        }
    }
}
