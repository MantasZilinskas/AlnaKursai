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
    public class CategoryProvider : IAsyncDataProvider<CategoryDAO>
    {
        private readonly TodoAppContext _context;

        public CategoryProvider(TodoAppContext context)
        {
            _context = context;
        }
        public async Task<int> Create(CategoryDAO data)
        {
            if(data.Name.Length <= 2)
            {
                throw new ArgumentException("Category name must be longer than 2 letters");
            }
            _context.Categories.Add(data);
            await _context.SaveChangesAsync();
            return data.Id;
        }

        public async Task Delete(int id)
        {
            _context.Categories.Remove(_context.Categories.Find(id));
            await _context.SaveChangesAsync();
        }

        public async Task<CategoryDAO> Get(int? id)
        {
            return await _context.Categories.FirstOrDefaultAsync(value => value.Id == id);
        }

        public async Task<IEnumerable<CategoryDAO>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task Update(CategoryDAO data)
        {
            CategoryDAO item = await _context.Categories.FirstOrDefaultAsync(value => value.Id == data.Id);
            item.Name = data.Name;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsDuplicate(CategoryDAO category)
        {
            return await _context.Categories.AnyAsync(value => value.Name == category.Name);
        }
        public async Task<bool> Exists(int? id)
        {
            return await _context.Categories.AnyAsync(value => value.Id == id);
        }
    }
}
