using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.Service.Commands.Messaging.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EventHub.Service.Commands.Handlers.Events
{
    public sealed class LeaveEventCommandHandler : AsyncRequestHandler<LeaveEventCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _applicationDbContext;

        public LeaveEventCommandHandler(IHttpContextAccessor httpContextAccessor, ApplicationDbContext applicationDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationDbContext = applicationDbContext;
        }

        protected async override Task Handle(LeaveEventCommand request, CancellationToken cancellationToken)
        {
            var eventObject = await _applicationDbContext.Events.Include(e => e.Members).FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (eventObject == null)
            {
                throw new KeyNotFoundException("Event could not be found");
            }

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var eventMember = eventObject.Members.FirstOrDefault(m => m.UserId == userId);
            if (eventMember == null)
            {
                throw new KeyNotFoundException("Member could not be found");
            }

            eventObject.Members.Remove(eventMember);

            if (eventObject.Members.Any())
            {
                _applicationDbContext.EventMembers.Remove(eventMember);
                var oldestMember = eventObject.Members.OrderBy(em => em.CreatedAt).First();
                oldestMember.IsAdmin = true;
            }
            else
            {
                _applicationDbContext.Events.Remove(eventObject);
            }

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
