using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Data.Models
{
    public class TagDAO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<ItemTagDAO> ItemTags { get; set; }
    }
}
