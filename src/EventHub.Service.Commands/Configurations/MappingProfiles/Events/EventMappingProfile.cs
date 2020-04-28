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
                .ForMember(dest => dest.StartDate, src => src.MapFrom(s => s.StartDate.ToUniversalTime().Date))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(s => s.EndDate.ToUniversalTime().Date))
                .ForMember(dest => dest.Members, src => src.Ignore())
                .ForMember(dest => dest.Messages, src => src.Ignore())
                .AfterMap((src, dest, context) =>
                {
                    dest.Members.Add(context.Mapper.Map<EventMember>(src));
                });

            CreateMap<UpdateEventCommand, Event>()
                .ForMember(dest => dest.CreatedAt, src => src.Ignore())
                .ForMember(dest => dest.CreatedByUserId, src => src.Ignore())
                .ForMember(dest => dest.CreatedByUser, src => src.Ignore())
                .ForMember(dest => dest.ModifiedAt, src => src.MapFrom(s => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedByUser, src => src.Ignore())
                .ForMember(dest => dest.ModifiedByUserId, src => src.MapFrom<UserIdValueResolver>())
                .ForMember(dest => dest.StartDate, src => src.MapFrom(s => s.StartDate.ToUniversalTime().Date))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(s => s.EndDate.ToUniversalTime().Date))
                .ForMember(dest => dest.Members, src => src.Ignore())
                .ForMember(dest => dest.Messages, src => src.Ignore());

        }
    }
}
