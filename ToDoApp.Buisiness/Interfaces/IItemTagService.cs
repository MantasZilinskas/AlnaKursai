using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Buisiness.Interfaces
{
    public interface IItemTagService
    {
        public Task Create(int todoItemId, List<int> tagIdList);
        public Task Delete(int todoItemId);
        public Task Update(int todoItemId, List<int> tagIdList);
    }
}
