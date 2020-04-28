using AutoMapper;
using System;

namespace EventHub.Service.Commands.Configurations.MappingProfiles.Common
{
    public class DateTimeMappingProfile : Profile
    {
        public DateTimeMappingProfile()
        {
            CreateMap<DateTime, DateTimeOffset>().ConvertUsing(d => d.ToUniversalTime());
            CreateMap<DateTime?, DateTimeOffset?>().ConvertUsing(d => d.Value.ToUniversalTime());
        }
    }
}
