using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Buisiness.Models
{
    public class ItemTagVO
    {
        public int TagId { get; set; }
        public TagVO Tag { get; set; }
        public int TodoItemId { get; set; }
        public TodoItemVO TodoItem { get; set; }
    }
}
