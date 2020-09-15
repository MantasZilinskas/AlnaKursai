using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Data.Models;

namespace TodoApp.Data.Interfaces
{
    public interface ITodoItemProvider
    {
        public Task<int> Create(TodoItemDAO data);
        public Task<IEnumerable<TodoItemDAO>> GetAll();
        public Task<TodoItemDAO> Get(int? id);
        public Task Update(TodoItemDAO data);
        public Task Delete(int id);
        public Task<bool> IsDuplicate(TodoItemDAO data);
        public Task<bool> Exists(int? id);
        public Task<bool> WipStatusWithPriority1Exists();
        public Task<bool> ThreeItemsOfWipStatusWithPriority2Exists();
    }
}
