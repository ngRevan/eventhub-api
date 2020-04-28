using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.Service.Queries.Messaging.Common;
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
using X.PagedList;

namespace EventHub.Service.Queries.Handlers.Events
{
    public sealed class MessageListQueryHandler : IRequestHandler<MessageListQuery, MessageListResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IMapper _mapper;

        public MessageListQueryHandler(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext, IConfigurationProvider configurationProvider, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _configurationProvider = configurationProvider;
            _mapper = mapper;
        }

        public async Task<MessageListResult> Handle(MessageListQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (await _dbContext.Events.AnyAsync(e => e.Id == request.EventId && e.Members.Any(em => em.UserId == userId)) == false)
            {
                throw new KeyNotFoundException("Event could not be found");
            }

            var pageSize = request.PageSize ?? 100;
            if (pageSize > 100)
            {
                pageSize = 100;
            }
            var pageNumber = request.PageNumber ?? 1;
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var query = _dbContext.Messages
                .AsNoTracking()
                .Where(m => m.EventId == request.EventId)
                .OrderByDescending(m => m.CreatedAt);

            var count = await query.CountAsync(cancellationToken);

            var result = await query.ProjectTo<MessageView>(_configurationProvider).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            result.Reverse();
            var pagedList = new StaticPagedList<MessageView>(result, pageNumber, pageSize, count);

            return new MessageListResult
            {
                Result = _mapper.Map<PagedListResult<MessageView>>(pagedList),
            };
        }
    }
}
