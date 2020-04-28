using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace EventHub.Web.Api.Hubs
{
    public class NameIdentifierUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connectionContext)
        {
            var user = connectionContext?.User;
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
