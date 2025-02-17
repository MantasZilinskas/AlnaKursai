﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Web.ViewModels
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<ItemTagViewModel> ItemTags { get; set; }
    }
}
