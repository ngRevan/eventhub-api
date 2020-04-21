using AutoMapper;
using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.DataAccess.EntityFramework.Models;
using EventHub.Service.Commands.Messaging.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EventHub.Service.Commands.Handlers.Events
{
    public sealed class JoinEventCommandHandler : AsyncRequestHandler<JoinEventCommand>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;

        public JoinEventCommandHandler(IMapper mapper, ApplicationDbContext applicationDbContext)
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        protected async override System.Threading.Tasks.Task Handle(JoinEventCommand request, CancellationToken cancellationToken)
        {
            var eventObject = await _applicationDbContext.Events.Include(e => e.Members).FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (eventObject == null)
            {
                throw new KeyNotFoundException("Event could not be found");
            }

            var eventMember = _mapper.Map<JoinEventCommand, EventMember>(request);
            if (eventObject.Members.Any(m => m.UserId == eventMember.UserId))
            {
                return;
            }

            eventObject.Members.Add(eventMember);
            await _applicationDbContext.EventMembers.AddAsync(eventMember, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
