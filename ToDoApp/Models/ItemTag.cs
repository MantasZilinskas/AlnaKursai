using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
    public class ItemTag
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int TodoItemId { get; set; }
        public TodoItem TodoItem { get; set; }
    }
}
