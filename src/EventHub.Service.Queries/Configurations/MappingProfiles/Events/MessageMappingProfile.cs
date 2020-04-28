using AutoMapper;
using EventHub.DataAccess.EntityFramework.Models;
using EventHub.Service.Queries.ViewModels;

namespace EventHub.Service.Queries.Configurations.MappingProfiles.Events
{
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            CreateMap<Message, MessageView>()
                .ForMember(dest => dest.Username, src => src.MapFrom(s => s.CreatedByUser == null ? "Unknown" : s.CreatedByUser.UserName));
        }
    }
}
