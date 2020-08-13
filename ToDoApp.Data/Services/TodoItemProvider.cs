using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data.Context;
using TodoApp.Data.Interfaces;
using TodoApp.Data.Models;

namespace TodoApp.Data.Services
{
    public class TodoItemProvider : IAsyncDataProvider<TodoItemDAO>
    {
        private readonly ToDoAppContext _context;

        public TodoItemProvider(ToDoAppContext context)
        {
            _context = context;
        }
        public async Task<int> Create(TodoItemDAO data)
        {
            TodoItemDAO item = await _context.TodoItems.FirstOrDefaultAsync(value => value.Name == data.Name);
            if (item == null)
            {
                TodoItemDAO addedItem = _context.TodoItems.Add(data).Entity;
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
            TodoItemDAO item = await _context.TodoItems.FirstOrDefaultAsync(value => value.Id == id);
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

        public async Task<TodoItemDAO> Get(int? id)
        {
            TodoItemDAO item = await _context.TodoItems.Include(item => item.ItemTags).ThenInclude(item => item.Tag).FirstOrDefaultAsync(value => value.Id == id);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<ICollection<TodoItemDAO>> GetAll()
        {
            return await _context.TodoItems.Include(item => item.Category).ToListAsync();
        }

        public async Task Update(TodoItemDAO data)
        {
            TodoItemDAO item = await _context.TodoItems.FirstOrDefaultAsync(value => value.Id == data.Id);
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
