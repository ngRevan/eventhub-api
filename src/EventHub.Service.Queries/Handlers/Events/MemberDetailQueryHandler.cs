using AutoMapper;
using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.Service.Queries.Messaging.Events;
using EventHub.Service.Queries.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventHub.Service.Queries.Handlers.Events
{
    public sealed class MemberDetailQueryHandler : IRequestHandler<MemberDetailQuery, MemberDetailResult>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public MemberDetailQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MemberDetailResult> Handle(MemberDetailQuery request, CancellationToken cancellationToken)
        {
            var eventMember = await _dbContext.EventMembers.FirstOrDefaultAsync(em => em.EventId == request.EventId && em.UserId == request.UserId, cancellationToken);
            if (eventMember == null)
            {
                throw new KeyNotFoundException("Event member could not be found");
            }

            return new MemberDetailResult
            {
                Result = _mapper.Map<EventMemberView>(eventMember),
            };
        }
    }
}
