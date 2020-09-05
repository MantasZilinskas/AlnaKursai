﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TodoApp.ClientsAndProjects.Api.Data.Model
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
