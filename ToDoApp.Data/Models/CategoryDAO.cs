using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data.Interfaces;

namespace TodoApp.Data.Models
{
    public class CategoryDAO : IHasId
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
