using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.Service.Commands.Messaging.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace EventHub.Service.Commands.Handlers.Events
{
    public sealed class DeleteEventCommandHandler : AsyncRequestHandler<DeleteEventCommand>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteEventCommandHandler(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        protected async override System.Threading.Tasks.Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var eventObject = await _applicationDbContext.Events.Where(e => e.Members.Any(m => m.UserId == userId && m.IsAdmin)).FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (eventObject == null)
            {
                throw new KeyNotFoundException();
            }

            _applicationDbContext.Events.Remove(eventObject);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
