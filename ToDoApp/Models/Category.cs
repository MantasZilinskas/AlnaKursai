using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Interfaces;

namespace ToDoApp.Models
{
    public class Category : IHasId
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
