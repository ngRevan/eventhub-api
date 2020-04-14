using AutoMapper;
using EventHub.DataAccess.EntityFramework.Models;
using EventHub.Service.Commands.Mapping;
using EventHub.Service.Commands.Messaging.Events;
using System;

namespace EventHub.Service.Commands.Configurations.MappingProfiles.Events
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<CreateEventCommand, Event>()
                .ForMember(dest => dest.CreatedAt, src => src.MapFrom(s => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedByUserId, src => src.MapFrom<UserIdValueResolver>())
                .ForMember(dest => dest.CreatedByUser, src => src.Ignore())
                .ForMember(dest => dest.ModifiedAt, src => src.Ignore())
                .ForMember(dest => dest.ModifiedByUser, src => src.Ignore())
                .ForMember(dest => dest.ModifiedByUserId, src => src.Ignore())
                .ForMember(dest => dest.Members, src => src.Ignore())
                .ForMember(dest => dest.Messages, src => src.Ignore())
                .AfterMap((src, dest, context) =>
                {
                    dest.Members.Add(context.Mapper.Map<EventMember>(src));
                });

            CreateMap<CreateEventCommand, EventMember>()
                .ForMember(dest => dest.Id, src => src.MapFrom(s => Guid.NewGuid()))
                .ForMember(dest => dest.IsAdmin, src => src.MapFrom(s => true))
                .ForMember(dest => dest.CreatedAt, src => src.MapFrom(s => DateTime.Now))
                .ForMember(dest => dest.CreatedByUserId, src => src.MapFrom<UserIdValueResolver>())
                .ForMember(dest => dest.UserId, src => src.MapFrom<UserIdValueResolver>())
                .ForAllOtherMembers(src => src.Ignore());

            CreateMap<UpdateEventCommand, Event>()
                .ForMember(dest => dest.CreatedAt, src => src.Ignore())
                .ForMember(dest => dest.CreatedByUserId, src => src.Ignore())
                .ForMember(dest => dest.CreatedByUser, src => src.Ignore())
                .ForMember(dest => dest.ModifiedAt, src => src.MapFrom(s => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedByUser, src => src.Ignore())
                .ForMember(dest => dest.ModifiedByUserId, src => src.MapFrom<UserIdValueResolver>())
                .ForMember(dest => dest.Members, src => src.Ignore())
                .ForMember(dest => dest.Messages, src => src.Ignore());

        }
    }
}
