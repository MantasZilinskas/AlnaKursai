using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models.Enums;

namespace ToDoApp.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }

        public TodoItem()
        {
            Priority = Priority.Default;
        }

        public override bool Equals(object obj)
        {
            return obj is TodoItem item &&
                   Name == item.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
