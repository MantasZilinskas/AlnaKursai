using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Category category &&
                   (Id == category.Id ||
                   Name == category.Name);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
