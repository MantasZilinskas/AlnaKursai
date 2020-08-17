using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TodoApp.Web.ViewModels.Enums;

namespace TodoApp.Web.ViewModels
{
    public class TodoItemViewModel
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
        public CategoryViewModel Category { get; set; }
        public int? CategoryId { get; set; }
        public Status Status { get; set; }

        public IList<ItemTagViewModel> ItemTags { get; set; }

        public TodoItemViewModel()
        {
            Priority = 3;
            Status = Status.Backlog;
            CreationDate = DateTime.UtcNow;
        }
    }
}
