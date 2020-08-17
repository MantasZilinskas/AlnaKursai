
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TodoApp.Data.Context;
using TodoApp.Data.Interfaces;
using TodoApp.Data.Models;

namespace TodoApp.Data.Providers
{
    public class ItemTagProvider : IItemTagProvider
    {
        private readonly ToDoAppContext _context;

        public ItemTagProvider(ToDoAppContext context)
        {
            _context = context;
        }
        public async Task Create(int todoItemId, List<int> tagIdList)
        {
            tagIdList.ForEach(tagId => _context.ItemTags.Add(new ItemTagDAO { TodoItemId = todoItemId, TagId = tagId }));
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int todoItemId)
        {
            List<ItemTagDAO> itemTags = await _context.ItemTags.Where(itemTag => itemTag.TodoItemId == todoItemId).ToListAsync();
            itemTags.ForEach(itemTag => _context.ItemTags.Remove(itemTag));
            await _context.SaveChangesAsync();
        }
        public async Task Update(int todoItemId, List<int> tagIdList)
        {
            await Delete(todoItemId);
            await Create(todoItemId, tagIdList);
        }
    }
}
