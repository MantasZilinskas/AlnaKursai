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
    public class TagProvider : IAsyncDataProvider<TagDAO>
    {
        private readonly ToDoAppContext _context;

        public TagProvider(ToDoAppContext context)
        {
            _context = context;
        }
        public async Task<int> Create(TagDAO data)
        {
            TagDAO addedTag = _context.Tags.Add(data).Entity;
            await _context.SaveChangesAsync();
            return addedTag.Id;
        }
        public async Task Delete(int id)
        {
            _context.Tags.Remove(_context.Tags.Find(id));
            await _context.SaveChangesAsync();
        }

        public async Task<TagDAO> Get(int? id)
        {
            return await _context.Tags.FirstOrDefaultAsync(value => value.Id == id);
        }

        public async Task<IEnumerable<TagDAO>> GetAll()
        {
            return await _context.Tags.Include(tag => tag.ItemTags).ToListAsync();
        }

        public async Task Update(TagDAO data)
        {
            TagDAO tag = await _context.Tags.FirstOrDefaultAsync(value => value.Id == data.Id);
            tag.Name = data.Name;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsDuplicate(TagDAO tag)
        {
            return await _context.Tags.AnyAsync(value => value.Name == tag.Name);
        }
        public async Task<bool> Exists(int? id)
        {
            return await _context.Tags.AnyAsync(value => value.Id == id);
        }
    }
}

