using EventHub.Service.Queries.ViewModels;
using System.Collections.Generic;

namespace EventHub.Service.Queries.Messaging.Events
{
    public sealed class MessageListResult
    {
        public IEnumerable<MessageView> Results { get; set; }
    }
}
