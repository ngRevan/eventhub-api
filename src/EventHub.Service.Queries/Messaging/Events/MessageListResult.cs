using EventHub.Service.Queries.Messaging.Common;
using EventHub.Service.Queries.ViewModels;

namespace EventHub.Service.Queries.Messaging.Events
{
    public sealed class MessageListResult
    {
        public PagedListResult<MessageView> Result { get; set; }
    }
}
