using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.Service.Queries.Messaging.Events;
using EventHub.Service.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EventHub.Service.Queries.Handlers.Events
{
    public sealed class MessageListQueryHandler : IRequestHandler<MessageListQuery, MessageListResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfigurationProvider _configurationProvider;

        public MessageListQueryHandler(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext, IConfigurationProvider configurationProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _configurationProvider = configurationProvider;
        }

        public async Task<MessageListResult> Handle(MessageListQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (await _dbContext.Events.AnyAsync(e => e.Id == request.EventId && e.Members.Any(em => em.UserId == userId)) == false)
            {
                throw new KeyNotFoundException("Event could not be found");
            }

            var query = _dbContext.Messages
                .AsNoTracking()
                .Where(m => m.EventId == request.EventId)
                .OrderBy(m => m.CreatedAt);

            var count = await query.CountAsync(cancellationToken);

            var result = await query.ProjectTo<MessageView>(_configurationProvider).ToListAsync(cancellationToken);

            return new MessageListResult
            {
                Results = result,
            };
        }
    }
}
