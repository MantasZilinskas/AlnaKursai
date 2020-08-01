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
    public class TagProvider : IAsyncDataProvider<Tag>
    {
        private readonly ToDoAppContext _context;

        public TagProvider(ToDoAppContext context)
        {
            _context = context;
        }
        public async Task Create(Tag data)
        {
            Tag tag = await _context.Tags.FirstOrDefaultAsync(value => value.Name == data.Name);
            if (tag == null)
            {
                _context.Tags.Add(data);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("A tag with the name " + data.Name + " already exists");
            }
        }

        public async Task Delete(int id)
        {
            Tag tag = await _context.Tags.FirstOrDefaultAsync(value => value.Id == id);
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

        public async Task<Tag> Get(int? id)
        {
            Tag tag = await _context.Tags.FirstOrDefaultAsync(value => value.Id == id);
            if (tag != null)
            {
                return tag;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<ICollection<Tag>> GetAll()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task Update(Tag data)
        {
            Tag tag = await _context.Tags.FirstOrDefaultAsync(value => value.Id == data.Id);
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

