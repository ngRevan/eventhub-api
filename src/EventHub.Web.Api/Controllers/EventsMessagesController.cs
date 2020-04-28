using EventHub.Service.Queries.Messaging.Common;
using EventHub.Service.Queries.Messaging.Events;
using EventHub.Service.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<ActionResult<PagedListResult<MessageView>>> Get(Guid eventId, [FromQuery]MessageListQuery message)
        {
            message.EventId = eventId;
            var result = await _mediator.Send(message);

            return Ok(result.Result);
        }
    }
}
