using AutoMapper;
using TodoApp.Buisiness.Models;
using TodoApp.Data.Models;
using TodoApp.Web.ViewModels;

namespace TodoApp.Web.Mapping
{
    public class TodoItemViewModelProfile : Profile
    {
        public TodoItemViewModelProfile()
        {
            CreateMap<TodoItemVO, TodoItemViewModel>()
                .ReverseMap();
        }
    }
}
