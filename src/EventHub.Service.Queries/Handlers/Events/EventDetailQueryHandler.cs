﻿using AutoMapper;
using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.DataAccess.EntityFramework.Models;
using EventHub.Service.Queries.Messaging.Events;
using EventHub.Service.Queries.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventHub.Service.Queries.Handlers.Events
{
    public sealed class EventDetailQueryHandler : IRequestHandler<EventDetailQuery, EventDetailResult>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public EventDetailQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EventDetailResult> Handle(EventDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Events.AsNoTracking().FirstOrDefaultAsync(e => e.Id == request.Id);
            if (result == null)
            {
                throw new KeyNotFoundException("Event could not be found.");
            }

            return new EventDetailResult
            {
                Result = _mapper.Map<Event, EventView>(result),
            };
        }
    }
}
