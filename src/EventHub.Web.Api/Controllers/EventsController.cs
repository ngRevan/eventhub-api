using EventHub.Service.Queries.Messaging.Events;
using EventHub.Service.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    }
}
