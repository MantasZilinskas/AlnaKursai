using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TodoApp.ClientsAndProjects.Api.Data.Model
{
    public class ClientDAO
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        public IEnumerable<ProjectDAO> Projects { get; set; }
    }
}
