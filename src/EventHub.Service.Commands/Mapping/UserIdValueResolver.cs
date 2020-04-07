using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EventHub.Service.Commands.Mapping
{
    public class UserIdValueResolver : IValueResolver<object, object, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdValueResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(object source, object destination, string destMember, ResolutionContext context)
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
