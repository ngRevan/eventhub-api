using AutoMapper;
using EventHub.DataAccess.EntityFramework.Models;
using EventHub.Service.Commands.Mapping;
using EventHub.Service.Commands.Messaging.Events;
using System;

namespace EventHub.Service.Commands.Configurations.MappingProfiles.Events
{
    public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            CreateMap<CreateMessageCommand, Message>()
                .ForMember(dest => dest.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedByUserId, src => src.MapFrom<UserIdValueResolver>())
                .ForMember(dest => dest.CreatedByUser, src => src.Ignore())
                .ForMember(dest => dest.ModifiedAt, src => src.Ignore())
                .ForMember(dest => dest.ModifiedByUser, src => src.Ignore())
                .ForMember(dest => dest.ModifiedByUserId, src => src.Ignore())
                .ForMember(dest => dest.Event, src => src.Ignore());
        }
    }
}
