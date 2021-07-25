using AutoMapper;
using SOPopularTags.Application.ViewModels;
using SOPopularTags.Domain.Models;

namespace SOPopularTags.Application.Mapping
{
    public class SOTagRequestProfile : Profile
    {
        public SOTagRequestProfile()
        {
            CreateMap<SOTagRequest, HomeVM>()
                .ForMember(x => x.Items, y => y.MapFrom(src => src.Items));
            CreateMap<SOTagRequestItem, PopularTagForHomeVM>();
        }
    }
}
