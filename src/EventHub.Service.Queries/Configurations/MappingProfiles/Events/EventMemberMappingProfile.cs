using AutoMapper;
using EventHub.DataAccess.EntityFramework.Models;
using EventHub.Service.Queries.ViewModels;

namespace EventHub.Service.Queries.Configurations.MappingProfiles.Events
{
    public class EventMemberMappingProfile : Profile
    {
        public EventMemberMappingProfile()
        {
            CreateMap<EventMember, EventMemberView>()
                .ForMember(dest => dest.Username, src => src.MapFrom(s => s.User.UserName));
        }
    }
}
