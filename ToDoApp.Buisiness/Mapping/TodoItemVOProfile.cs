using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Buisiness.Models;
using TodoApp.Data.Models;

namespace TodoApp.Buisiness.Mapping
{
    public class TodoItemVOProfile : Profile
    {
        public TodoItemVOProfile()
        {
            CreateMap<TodoItemDAO, TodoItemVO>()
                .ReverseMap();
        }
    }
}
