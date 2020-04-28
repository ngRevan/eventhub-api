using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.Service.Queries.Messaging.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventHub.Service.Queries.Handlers.Events
{
    public sealed class IsEventMemberQueryHandler : IRequestHandler<IsEventMemberQuery, IsEventMemberResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public IsEventMemberQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IsEventMemberResult> Handle(IsEventMemberQuery request, CancellationToken cancellationToken)
        {
            var isMember = await _dbContext.Events.AnyAsync(e => e.Id == request.EventId && e.Members.Any(em => em.UserId == request.UserId), cancellationToken);

            return new IsEventMemberResult
            {
                Result = isMember,
            };
        }
    }
}
