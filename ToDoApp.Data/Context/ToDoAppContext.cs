using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data.Models;

namespace TodoApp.Data.Context
{
    public class ToDoAppContext : DbContext
    {
        public ToDoAppContext (DbContextOptions<ToDoAppContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItemDAO> TodoItems { get; set; }
        public DbSet<CategoryDAO> Categories { get; set; }
        public DbSet<TagDAO> Tags { get; set; }
        public DbSet<ItemTagDAO> ItemTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region ItemTag

            modelBuilder.Entity<ItemTagDAO>().HasKey(sc => new { sc.TodoItemId, sc.TagId });

            modelBuilder.Entity<ItemTagDAO>()
                .HasOne<TodoItemDAO>(sc => sc.TodoItem)
                .WithMany(s => s.ItemTags)
                .HasForeignKey(sc => sc.TodoItemId);

            modelBuilder.Entity<ItemTagDAO>()
                .HasOne<TagDAO>(sc => sc.Tag)
                .WithMany(s => s.ItemTags)
                .HasForeignKey(sc => sc.TagId);

            #endregion
        }
    }
}
