using AutoMapper;
using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.DataAccess.EntityFramework.Models;
using EventHub.Service.Commands.Messaging.Events;
using MediatR;
using System.Threading;

namespace EventHub.Service.Commands.Handlers.Events
{
    public sealed class CreateEventCommandHandler : AsyncRequestHandler<CreateEventCommand>
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;

        public CreateEventCommandHandler(IMapper mapper, ApplicationDbContext applicationDbContext)
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }

        protected async override System.Threading.Tasks.Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventObject = _mapper.Map<Event>(request);

            await _applicationDbContext.Events.AddAsync(eventObject, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
