using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Buisiness.Interfaces;

namespace TodoApp.Buisiness.Models
{
    public class CategoryVO : IHasId
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
