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
    [Route("api/Events/{eventId:guid}/Messages")]
    [ResponseCache(CacheProfileName = "Never")]
    public class EventsMessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsMessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "getEventMessages")]
        public async Task<ActionResult<IEnumerable<MessageView>>> Get(Guid eventId)
        {
            var result = await _mediator.Send(new MessageListQuery { EventId = eventId });

            return Ok(result.Results);
        }
    }
}
