using AutoMapper;
using EventHub.DataAccess.EntityFramework.Models;
using EventHub.Service.Commands.Mapping;
using EventHub.Service.Commands.Messaging.Events;
using System;

namespace EventHub.Service.Commands.Configurations.MappingProfiles.Events
{
    public class EventMemberMappingProfile : Profile
    {
        public EventMemberMappingProfile()
        {
            CreateMap<CreateEventCommand, EventMember>()
                .ForMember(dest => dest.Id, src => src.MapFrom(s => Guid.NewGuid()))
                .ForMember(dest => dest.IsAdmin, src => src.MapFrom(s => true))
                .ForMember(dest => dest.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedByUserId, src => src.MapFrom<UserIdValueResolver>())
                .ForMember(dest => dest.UserId, src => src.MapFrom<UserIdValueResolver>())
                .ForAllOtherMembers(src => src.Ignore());

            CreateMap<JoinEventCommand, EventMember>()
                .ForMember(dest => dest.Id, src => src.MapFrom(s => Guid.NewGuid()))
                .ForMember(dest => dest.IsAdmin, src => src.MapFrom(s => false))
                .ForMember(dest => dest.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedByUserId, src => src.MapFrom<UserIdValueResolver>())
                .ForMember(dest => dest.UserId, src => src.MapFrom<UserIdValueResolver>())
                .ForAllOtherMembers(src => src.Ignore());

            CreateMap<UpdateMemberCommand, EventMember>(MemberList.Source);
        }
    }
}
