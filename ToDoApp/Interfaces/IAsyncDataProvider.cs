using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Interfaces
{
    public interface IAsyncDataProvider<TDataClass>
    {
        public Task<int> Create(TDataClass data);
        public Task<ICollection<TDataClass>> GetAll();
        public Task<TDataClass> Get(int? id);
        public Task Update(TDataClass data);
        public Task Delete(int id);
    }
}
