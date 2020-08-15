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
    public class CategoryProvider : IAsyncDataProvider<CategoryDAO>
    {
        private readonly ToDoAppContext _context;

        public CategoryProvider(ToDoAppContext context)
        {
            _context = context;
        }
        public async Task<int> Create(CategoryDAO data)
        {
            CategoryDAO item = await _context.Categories.FirstOrDefaultAsync(value => value.Name == data.Name);
            if (item == null)
            {
                CategoryDAO addedCategory = _context.Categories.Add(data).Entity;
                await _context.SaveChangesAsync();
                return addedCategory.Id;
            }
            else
            {
                throw new ArgumentException("A category with the name " + data.Name + " already exists");
            }
        }

        public async Task Delete(int id)
        {
            CategoryDAO item = await _context.Categories.FirstOrDefaultAsync(value => value.Id == id);
            if (item != null)
            {
                _context.Categories.Remove(item);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<CategoryDAO> Get(int? id)
        {
            CategoryDAO item = await _context.Categories.FirstOrDefaultAsync(value => value.Id == id);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<IEnumerable<CategoryDAO>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task Update(CategoryDAO data)
        {
            CategoryDAO item = await _context.Categories.FirstOrDefaultAsync(value => value.Id == data.Id);
            if (item != null)
            {
                item.Name = data.Name;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
