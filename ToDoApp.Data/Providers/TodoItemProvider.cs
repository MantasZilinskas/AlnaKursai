using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data.Context;
using TodoApp.Data.Interfaces;
using TodoApp.Data.Models;

namespace TodoApp.Data.Providers
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
            TodoItemDAO addedItem = _context.TodoItems.Add(data).Entity;
            await _context.SaveChangesAsync();
            return addedItem.Id;
        }

        public async Task Delete(int id)
        {
            _context.TodoItems.Remove(_context.TodoItems.Find(id));
            await _context.SaveChangesAsync();
        }

        public async Task<TodoItemDAO> Get(int? id)
        {
            return await _context.TodoItems
                .Include(item => item.ItemTags)
                .ThenInclude(item => item.Tag)
                .FirstOrDefaultAsync(value => value.Id == id);
        }

        public async Task<IEnumerable<TodoItemDAO>> GetAll()
        {
            return await _context.TodoItems.Include(item => item.Category).ToListAsync();
        }

        public async Task Update(TodoItemDAO data)
        {
            TodoItemDAO item = await _context.TodoItems.FirstOrDefaultAsync(value => value.Id == data.Id);
            item.Name = data.Name;
            item.Description = data.Description;
            item.Priority = data.Priority;
            item.CategoryId = data.CategoryId;
            item.Status = data.Status;
            item.DeadLineDate = data.DeadLineDate;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsDuplicate(TodoItemDAO data)
        {
            return await _context.TodoItems.AnyAsync(item => item.Name == data.Name);
        }
        public async Task<bool> Exists(int? id)
        {
            return await _context.TodoItems.AnyAsync(value => value.Id == id);
        }
    }
}
