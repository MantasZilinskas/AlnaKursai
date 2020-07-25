using System;
using System.Collections.Generic;
using System.Linq;
using ToDoApp.Interfaces;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class InMemoryTodoItemProvider : IDataProvider<TodoItem>
    {
        private static List<TodoItem> values = new List<TodoItem>();

        public void Create(TodoItem data)
        {
            data.Id = values.Count();
            TodoItem item = values.FirstOrDefault(value => value.Name == data.Name);
            if(item == null)
            {
                values.Add(data);
            }
            else
            {
                throw new ArgumentException("An item with the name "+ data.Name +" already exists");
            }
        }

        public void Delete(int id)
        {
            TodoItem item = values.FirstOrDefault(value => value.Id == id);
            if(item != null)
            {
                values.Remove(item);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public TodoItem Get(int id)
        {
            TodoItem item = values.FirstOrDefault(value => value.Id == id);
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

        public void Update(TodoItem data)
        {
            TodoItem item = values.FirstOrDefault(value => value.Id == data.Id);
            if (item != null)
            {
                item.Name = data.Name;
                item.Description = data.Description;
                item.Priority = data.Priority;
                item.CategoryId = data.CategoryId;
                item.Status = data.Status;
                item.DeadLineDate = data.DeadLineDate;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
