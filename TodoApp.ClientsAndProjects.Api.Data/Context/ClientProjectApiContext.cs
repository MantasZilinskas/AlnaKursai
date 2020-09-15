using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApp.ClientsAndProjects.Api.Data.Model;

namespace TodoApp.ClientsAndProjects.Api.Data.Context
{
    public class ClientProjectApiContext : DbContext
    {
        public ClientProjectApiContext(DbContextOptions<ClientProjectApiContext> options)
            : base(options)
        {
        }

        public DbSet<ClientDAO> Clients { get; set; }

        public DbSet<ProjectDAO> Projects { get; set; }
    }
}
