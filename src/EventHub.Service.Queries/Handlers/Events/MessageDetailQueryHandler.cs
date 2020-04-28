using AutoMapper;
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
    public sealed class MessageDetailQueryHandler : IRequestHandler<MessageDetailQuery, MessageDetailResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public MessageDetailQueryHandler(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MessageDetailResult> Handle(MessageDetailQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var message = await _dbContext.Messages.FirstOrDefaultAsync(m => m.EventId == request.EventId && m.Id == request.Id && m.Event.Members.Any(em => em.UserId == userId), cancellationToken);
            if (message == null)
            {
                throw new KeyNotFoundException("Message could not be found");
            }

            return new MessageDetailResult
            {
                Result = _mapper.Map<MessageView>(message),
            };
        }
    }
}
