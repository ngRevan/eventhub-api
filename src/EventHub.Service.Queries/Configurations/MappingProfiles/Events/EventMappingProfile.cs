using AutoMapper;
using EventHub.DataAccess.EntityFramework.Models;
using EventHub.Service.Queries.ViewModels;

namespace EventHub.Service.Queries.Configurations.MappingProfiles.Events
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Event, EventView>();
        }
    }
}
