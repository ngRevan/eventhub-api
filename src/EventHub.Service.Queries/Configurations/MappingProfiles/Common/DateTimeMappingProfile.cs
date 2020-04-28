using AutoMapper;
using System;

namespace EventHub.Service.Queries.Configurations.MappingProfiles.Common
{
    public class DateTimeMappingProfile : Profile
    {
        public DateTimeMappingProfile()
        {
            CreateMap<DateTimeOffset, DateTime>().ConvertUsing(d => d.UtcDateTime);
            CreateMap<DateTimeOffset?, DateTime?>().ConvertUsing(d => d.Value.UtcDateTime);
        }
    }
}
