using System.Collections.Generic;
using ToDoApp.Models;

namespace ToDoApp.Interfaces
{
    public interface IInMemoryTodoItemProvider
    {
        public TodoItem Get(int Id);
        public ICollection<TodoItem> GetAll();
        public void Create(TodoItem data);
        public void Update(TodoItem data, int Id);
        public void Delete(TodoItem data, int Id);
    }
}
