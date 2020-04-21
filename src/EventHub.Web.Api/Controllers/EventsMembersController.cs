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
    [Route("api/Events/{eventId:guid}/Members")]
    [ResponseCache(CacheProfileName = "Never")]
    public class EventsMembersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsMembersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "getEventMembers")]
        public async Task<ActionResult<IEnumerable<EventMemberView>>> Get(Guid eventId)
        {
            var result = await _mediator.Send(new MemberListQuery { EventId = eventId });

            return Ok(result.Results);
        }

        [HttpGet("{userId}", Name = "getEventMemberByUserId")]
        public async Task<ActionResult<EventMemberView>> Get(Guid eventId, string userId)
        {
            var result = await _mediator.Send(new MemberDetailQuery { EventId = eventId, UserId = userId });

            return Ok(result.Result);
        }

        [HttpPut("{userId}", Name = "updateEventMember")]
        public async Task<IActionResult> Put(Guid eventId, string userId, [FromBody]UpdateMemberCommand message)
        {
            message.EventId = eventId;
            message.UserId = userId;

            await _mediator.Send(message);

            return NoContent();
        }
    }
}
