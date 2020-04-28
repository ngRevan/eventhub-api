using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.DataAccess.EntityFramework.Models;
using EventHub.Service.Queries.Messaging.Events;
using EventHub.Service.Queries.ViewModels;
using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EventHub.Service.Queries.Handlers.Events
{
    public sealed class EventListQueryHandler : IRequestHandler<EventListQuery, EventListResult>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventListQueryHandler(ApplicationDbContext dbContext, IConfigurationProvider configurationProvider, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _configurationProvider = configurationProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<EventListResult> Handle(EventListQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Events.AsNoTracking();
            query = ApplyFilter(query, request);

            var results = await query.ProjectTo<EventView>(_configurationProvider).ToListAsync(cancellationToken);

            return new EventListResult
            {
                Results = results,
            };
        }

        private IQueryable<Event> ApplyFilter(IQueryable<Event> query, EventListQuery message)
        {
            var predicate = PredicateBuilder.New<Event>(true);

            if (string.IsNullOrEmpty(message.SearchText) == false)
            {
                var searchTextPredicate = PredicateBuilder.New<Event>(false);
                var searchText = message.SearchText.ToLower();

                searchTextPredicate.Or(e => e.Name.ToLower().Contains(searchText));
                searchTextPredicate.Or(e => e.Description.ToLower().Contains(searchText));

                predicate.And(searchTextPredicate);
            }

            if (string.IsNullOrEmpty(message.Name) == false)
            {
                predicate.And(e => e.Name.ToLower().Contains(message.Name.ToLower()));
            }

            if (string.IsNullOrEmpty(message.Description) == false)
            {
                predicate.And(e => e.Description.ToLower().Contains(message.Description.ToLower()));
            }

            if(message.DateTimeFrom.HasValue)
            {
                predicate.And(e => e.StartDateTime >= message.DateTimeFrom.Value);
            }

            if (message.DateTimeTo.HasValue)
            {
                predicate.And(e => e.EndDateTime <= message.DateTimeTo.Value);
            }

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (message.IsMember.HasValue)
            {
                predicate.And(e => e.Members.Any(em => em.UserId == userId) == message.IsMember.Value);
            }

            return query.AsExpandableEFCore().Where(predicate);
        }
    }
}
