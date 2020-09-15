using Microsoft.EntityFrameworkCore;
using TodoApp.Data.Models;

namespace TodoApp.Data.Context
{
    public class TodoAppContext : DbContext
    {
        public TodoAppContext()
        {
        }
        public TodoAppContext(DbContextOptions<TodoAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TodoItemDAO> TodoItems { get; set; }
        public virtual DbSet<CategoryDAO> Categories { get; set; }
        public virtual DbSet<TagDAO> Tags { get; set; }
        public virtual DbSet<ItemTagDAO> ItemTags { get; set; }

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
