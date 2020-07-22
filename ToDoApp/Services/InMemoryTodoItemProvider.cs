using System;
using System.Collections.Generic;
using System.Linq;
using ToDoApp.Interfaces;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class InMemoryTodoItemProvider : IInMemoryTodoItemProvider
    {
        private static List<TodoItem> values = new List<TodoItem>();

        public void Create(TodoItem data)
        {
            TodoItem item = values.FirstOrDefault(value => value.Equals(data));
            if(item == null)
            {
                values.Add(data);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void Delete(TodoItem data, int Id)
        {
            TodoItem item = values.FirstOrDefault(value => value.Id == Id);
            if(item != null)
            {
                values.Remove(item);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public TodoItem Get(int Id)
        {
            TodoItem item = values.FirstOrDefault(value => value.Id == Id);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public ICollection<TodoItem> GetAll()
        {
            return values;
        }

        public void Update(TodoItem data, int Id)
        {
            TodoItem item = values.FirstOrDefault(value => value.Id == Id);
            if (item != null)
            {
                item.Name = data.Name;
                item.Description = data.Description;
                item.Priority = data.Priority;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
