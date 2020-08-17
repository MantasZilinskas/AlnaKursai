using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Web.ViewModels
{
    public class ItemTagViewModel
    {
        public int TagId { get; set; }
        public TagViewModel Tag { get; set; }
        public int TodoItemId { get; set; }
        public TodoItemViewModel TodoItem { get; set; }
    }
}
