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

        public DbSet<Client> Clients { get; set; }

        public DbSet<Project> Projects { get; set; }
    }
}
