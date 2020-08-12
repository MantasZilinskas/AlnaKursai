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
    public class CategoryProvider : IAsyncDataProvider<Category>
    {
        private readonly ToDoAppContext _context;

        public CategoryProvider(ToDoAppContext context)
        {
            _context = context;
        }
        public async Task<int> Create(Category data)
        {
            Category item = await _context.Categories.FirstOrDefaultAsync(value => value.Name == data.Name);
            if (item == null)
            {
                Category addedCategory = _context.Categories.Add(data).Entity;
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
            Category item = await _context.Categories.FirstOrDefaultAsync(value => value.Id == id);
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

        public async Task<Category> Get(int? id)
        {
            Category item = await _context.Categories.FirstOrDefaultAsync(value => value.Id == id);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<ICollection<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task Update(Category data)
        {
            Category item = await _context.Categories.FirstOrDefaultAsync(value => value.Id == data.Id);
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
