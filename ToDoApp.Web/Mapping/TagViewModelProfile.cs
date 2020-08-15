using AutoMapper;
using TodoApp.Buisiness.Models;
using TodoApp.Web.ViewModels;

namespace TodoApp.Web.Mapping
{
    public class TagViewModelProfile : Profile
    {
        public TagViewModelProfile()
        {
            CreateMap<TagVO, TagViewModel>()
                .ReverseMap();
        }
    }
}
