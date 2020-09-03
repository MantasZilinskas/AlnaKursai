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
    public class TodoItemProvider : ITodoItemProvider
    {
        private readonly TodoAppContext _context;

        public TodoItemProvider(TodoAppContext context)
        {
            _context = context;
        }
        public async Task<int> Create(TodoItemDAO data)
        {
            data.CreationDate = DateTime.UtcNow;
            if(data.CreationDate >= data.DeadLineDate)
            {
                throw new ArgumentException("Deadline date must be after the creation date");
            }
            if (await IsDuplicate(data))
            {
                throw new ArgumentException(String.Format("Item with the name {0} already exists", data.Name));
            }
            if(data.Priority > 5 || data.Priority < 1)
            {
                throw new ArgumentException("Priority value falls out of 1 to 5 range");
            }
            _context.TodoItems.Add(data);
            await _context.SaveChangesAsync();
            return data.Id;
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
            if (await IsDuplicate(data))
            {
                throw new ArgumentException(String.Format("Item with the name {0} already exists", data.Name));
            }
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
        public async Task<bool> WipStatusWithPriority1Exists()
        {
            return await _context.TodoItems.AnyAsync(value => value.Priority == 1 && value.Status == Models.Enums.Status.Wip);
        }
    }
}
