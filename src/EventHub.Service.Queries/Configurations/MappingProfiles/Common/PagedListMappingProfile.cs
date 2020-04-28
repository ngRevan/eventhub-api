using AutoMapper;
using EventHub.Service.Queries.Messaging.Common;
using X.PagedList;

namespace EventHub.Service.Queries.Configurations.MappingProfiles.Common
{
    public class PagedListMappingProfile : Profile
    {
        public PagedListMappingProfile()
        {
            CreateMap(typeof(IPagedList<>), typeof(PagedListResult<>))
                .ForMember(nameof(PagedListResult<object>.Items), src => src.MapFrom(s => s));
        }
    }
}
