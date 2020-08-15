using AutoMapper;
using TodoApp.Buisiness.Models;
using TodoApp.Web.ViewModels;

namespace TodoApp.Web.Mapping
{
    public class ItemTagViewModelProfile : Profile
    {
        public ItemTagViewModelProfile()
        {
            CreateMap<ItemTagVO,ItemTagViewModel>()
                .ReverseMap();
        }
    }
}
