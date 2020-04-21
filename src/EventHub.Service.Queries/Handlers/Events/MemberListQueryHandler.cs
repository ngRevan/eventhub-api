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
    public sealed class MemberListQueryHandler : IRequestHandler<MemberListQuery, MemberListResult>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfigurationProvider _configurationProvider;

        public MemberListQueryHandler(ApplicationDbContext dbContext, IConfigurationProvider configurationProvider)
        {
            _dbContext = dbContext;
            _configurationProvider = configurationProvider;
        }

        public async Task<MemberListResult> Handle(MemberListQuery request, CancellationToken cancellationToken)
        {
            if (await _dbContext.Events.AnyAsync(e => e.Id == request.EventId, cancellationToken) == false)
            {
                throw new KeyNotFoundException("Event could not be found");
            }

            var query = _dbContext.EventMembers.AsNoTracking().Include(em => em.User).Where(em => em.EventId == request.EventId);

            var results = await query.ProjectTo<EventMemberView>(_configurationProvider).ToListAsync(cancellationToken);

            return new MemberListResult
            {
                Results = results,
            };
        }
    }
}
