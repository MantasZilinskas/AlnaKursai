using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data.Interfaces;
using TodoApp.Data.Models.Enums;

namespace TodoApp.Data.Models
{
    public class TodoItemDAO : IHasId
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }
        [DisplayName("Deadline Date")]
        public DateTime? DeadLineDate { get; set; }
        [Required]
        [Range(1,5)]
        public int Priority { get; set; }
        public CategoryDAO Category { get; set; }
        public int? CategoryId { get; set; }
        public Status Status { get; set; }

        public IList<ItemTagDAO> ItemTags { get; set; }

        public TodoItemDAO()
        {
            Priority = 3;
            Status = Status.Backlog;
            CreationDate = DateTime.UtcNow;
        }
    }
}
