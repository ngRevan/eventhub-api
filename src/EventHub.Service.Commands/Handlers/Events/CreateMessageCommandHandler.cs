using AutoMapper;
using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.DataAccess.EntityFramework.Models;
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
    public sealed class CreateMessageCommandHandler : AsyncRequestHandler<CreateMessageCommand>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateMessageCommandHandler(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        protected async override System.Threading.Tasks.Task Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var eventObject = await _applicationDbContext.Events.FirstOrDefaultAsync(e => e.Members.Any(m => m.UserId == userId) && e.Id == request.EventId, cancellationToken);
            if (eventObject == null)
            {
                throw new KeyNotFoundException("Event could not be found");
            }

            var message = _mapper.Map<Message>(request);

            eventObject.Messages.Add(message);

            await _applicationDbContext.Messages.AddAsync(message, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
