using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.Buisiness.Models;
using TodoApp.Data.Models;

namespace TodoApp.Buisiness.Mapping
{
    public class ItemTagVOProfile : Profile
    {
        public ItemTagVOProfile()
        {
            CreateMap<ItemTagDAO, ItemTagVO>()
                .ReverseMap();
        }
    }
}
