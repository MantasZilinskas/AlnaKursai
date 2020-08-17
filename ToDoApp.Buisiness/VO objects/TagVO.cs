using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Buisiness.Models
{
    public class TagVO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<ItemTagVO> ItemTags { get; set; }
    }
}
