using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<ItemTag> ItemTags { get; set; }
    }
}
