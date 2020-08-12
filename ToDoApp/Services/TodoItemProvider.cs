using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Data;
using ToDoApp.Interfaces;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class TodoItemProvider : IAsyncDataProvider<TodoItem>
    {
        private readonly ToDoAppContext _context;

        public TodoItemProvider(ToDoAppContext context)
        {
            _context = context;
        }
        public async Task<int> Create(TodoItem data)
        {
            TodoItem item = await _context.TodoItems.FirstOrDefaultAsync(value => value.Name == data.Name);
            if (item == null)
            {
                TodoItem addedItem = _context.TodoItems.Add(data).Entity;
                await _context.SaveChangesAsync();
                return addedItem.Id;
            }
            else
            {
                throw new ArgumentException("An item with the name " + data.Name + " already exists");
            }
        }

        public async Task Delete(int id)
        {
            TodoItem item = await _context.TodoItems.FirstOrDefaultAsync(value => value.Id == id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<TodoItem> Get(int? id)
        {
            TodoItem item = await _context.TodoItems.Include(item => item.ItemTags).ThenInclude(item => item.Tag).FirstOrDefaultAsync(value => value.Id == id);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<ICollection<TodoItem>> GetAll()
        {
            return await _context.TodoItems.Include(item => item.Category).ToListAsync();
        }

        public async Task Update(TodoItem data)
        {
            TodoItem item = await _context.TodoItems.FirstOrDefaultAsync(value => value.Id == data.Id);
            if (item != null)
            {
                item.Name = data.Name;
                item.Description = data.Description;
                item.Priority = data.Priority;
                item.CategoryId = data.CategoryId;
                item.Status = data.Status;
                item.DeadLineDate = data.DeadLineDate;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
