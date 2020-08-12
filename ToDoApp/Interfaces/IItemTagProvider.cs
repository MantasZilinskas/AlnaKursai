using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Interfaces
{
    public interface IItemTagProvider
    {
        public Task Create(int todoItemId, List<int> tagIdList);
        public Task Delete(int todoItemId);
        public Task Update(int todoItemId, List<int> tagIdList);
    }
}
