using AutoMapper;
using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.Service.Commands.Messaging.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace EventHub.Service.Commands.Handlers.Events
{
    public sealed class UpdateMemberCommandHandler : AsyncRequestHandler<UpdateMemberCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;

        public UpdateMemberCommandHandler(IHttpContextAccessor httpContextAccessor, IMapper mapper, ApplicationDbContext applicationDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        protected async override System.Threading.Tasks.Task Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var eventObject = await _applicationDbContext.Events.Include(e => e.Members)
                .Where(e => e.Members.Any(em => em.UserId == userId && em.IsAdmin))
                .FirstOrDefaultAsync(e => e.Id == request.EventId, cancellationToken);

            if (eventObject == null)
            {
                throw new KeyNotFoundException("Event could not be found");
            }

            if (request.UserId == userId && request.IsAdmin == false)
            {
                throw new InvalidOperationException();
            }

            var eventMember = eventObject.Members.FirstOrDefault(em => em.UserId == request.UserId);
            if (eventMember == null)
            {
                throw new KeyNotFoundException("Event member could not be found");
            }

            _mapper.Map(request, eventMember);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
