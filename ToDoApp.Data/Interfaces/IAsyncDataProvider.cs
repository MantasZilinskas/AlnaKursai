using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Data.Interfaces
{
    public interface IAsyncDataProvider<TDataClass>
    {
        public Task<int> Create(TDataClass data);
        public Task<IEnumerable<TDataClass>> GetAll();
        public Task<TDataClass> Get(int? id);
        public Task Update(TDataClass data);
        public Task Delete(int id);
    }
}
