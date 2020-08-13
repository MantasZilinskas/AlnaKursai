﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Buisiness.Models.Enums;

namespace TodoApp.Buisiness.Models
{
    public class TodoItemVO : IHasId
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
        public CategoryVO Category { get; set; }
        public int? CategoryId { get; set; }
        public Status Status { get; set; }

        public IList<ItemTagVO> ItemTags { get; set; }

        public TodoItemVO()
        {
            Priority = 3;
            Status = Status.Backlog;
            CreationDate = DateTime.UtcNow;
        }
    }
}
