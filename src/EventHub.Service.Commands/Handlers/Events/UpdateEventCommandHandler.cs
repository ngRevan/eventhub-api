using AutoMapper;
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
    public sealed class UpdateEventCommandHandler : AsyncRequestHandler<UpdateEventCommand>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateEventCommandHandler(IMapper mapper, ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        protected async override System.Threading.Tasks.Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var eventObject = await _applicationDbContext.Events.Where(e => e.Members.Any(m => m.UserId == userId && m.IsAdmin)).FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (eventObject == null)
            {
                throw new KeyNotFoundException();
            }

            eventObject = _mapper.Map(request, eventObject);

            _applicationDbContext.Events.Update(eventObject);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
