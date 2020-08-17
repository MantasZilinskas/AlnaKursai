using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Data.Models
{
    public class ItemTagDAO
    {
        public int TagId { get; set; }
        public TagDAO Tag { get; set; }
        public int TodoItemId { get; set; }
        public TodoItemDAO TodoItem { get; set; }
    }
}
