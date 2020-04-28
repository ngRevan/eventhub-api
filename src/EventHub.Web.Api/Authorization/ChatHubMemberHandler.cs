using EventHub.Infrastructure.Authorization;
using EventHub.Service.Queries.Messaging.Events;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventHub.Web.Api.Authorization
{
    public class ChatHubMemberHandler : AuthorizationHandler<ChatHubMemberRequirement, HubInvocationContext>
    {
        private readonly IMediator _mediator;

        public ChatHubMemberHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ChatHubMemberRequirement requirement,
            HubInvocationContext resource)
        {
            var userId = resource.Context.UserIdentifier;
            var eventId = resource.HubMethodArguments.FirstOrDefault();
            if (eventId is Guid guidEventId)
            {
                var result = await _mediator.Send(new IsEventMemberQuery { EventId = guidEventId, UserId = userId });
                if (result.Result)
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
}
