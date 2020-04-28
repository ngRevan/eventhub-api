using EventHub.Infrastructure.Authorization;
using EventHub.Service.Commands.Messaging.Events;
using EventHub.Service.Queries.Messaging.Events;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace EventHub.Web.Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(AuthorizationPolicyNames.ChatHubMember)]
        public async Task SendMessage(Guid eventId, string message)
        {
            var messageId = Guid.NewGuid();
            await _mediator.Send(new CreateMessageCommand { EventId = eventId, Id = messageId, Text = message });

            var result = await _mediator.Send(new MessageDetailQuery { EventId = eventId, Id = messageId });
            await Clients.Group(eventId.ToString()).SendAsync("receiveMessage", result.Result);
        }

        [Authorize(AuthorizationPolicyNames.ChatHubMember)]
        public async Task JoinEventChat(Guid eventId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, eventId.ToString());
        }

        public Task LeaveEventChat(Guid eventId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, eventId.ToString());
        }
    }
}
