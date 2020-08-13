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
    public class TagProvider : IAsyncDataProvider<TagDAO>
    {
        private readonly ToDoAppContext _context;

        public TagProvider(ToDoAppContext context)
        {
            _context = context;
        }
        public async Task<int> Create(TagDAO data)
        {
            TagDAO tag = await _context.Tags.FirstOrDefaultAsync(value => value.Name == data.Name);
            if (tag == null)
            {
                TagDAO addedTag = _context.Tags.Add(data).Entity;
                await _context.SaveChangesAsync();
                return addedTag.Id;
            }
            else
            {
                throw new ArgumentException("A tag with the name " + data.Name + " already exists");
            }
        }

        public async Task Delete(int id)
        {
            TagDAO tag = await _context.Tags.FirstOrDefaultAsync(value => value.Id == id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<TagDAO> Get(int? id)
        {
            TagDAO tag = await _context.Tags.FirstOrDefaultAsync(value => value.Id == id);
            if (tag != null)
            {
                return tag;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<ICollection<TagDAO>> GetAll()
        {
            return await _context.Tags.Include(tag => tag.ItemTags).ToListAsync();
        }

        public async Task Update(TagDAO data)
        {
            TagDAO tag = await _context.Tags.FirstOrDefaultAsync(value => value.Id == data.Id);
            if (tag != null)
            {
                tag.Name = data.Name;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}

