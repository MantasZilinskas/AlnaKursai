using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class ToDoAppContext : DbContext
    {
        public ToDoAppContext (DbContextOptions<ToDoAppContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ItemTag> ItemTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region ItemTag

            modelBuilder.Entity<ItemTag>().HasKey(sc => new { sc.TodoItemId, sc.TagId });

            modelBuilder.Entity<ItemTag>()
                .HasOne<TodoItem>(sc => sc.TodoItem)
                .WithMany(s => s.ItemTags)
                .HasForeignKey(sc => sc.TodoItemId);

            modelBuilder.Entity<ItemTag>()
                .HasOne<Tag>(sc => sc.Tag)
                .WithMany(s => s.ItemTags)
                .HasForeignKey(sc => sc.TagId);

            #endregion
        }
    }
}
