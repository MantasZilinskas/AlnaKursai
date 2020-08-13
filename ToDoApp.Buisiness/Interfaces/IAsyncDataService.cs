using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Buisiness.Interfaces
{
    public interface IAsyncDataService<TDataClass>
    {
        public Task<int> Create(TDataClass Buisiness);
        public Task<ICollection<TDataClass>> GetAll();
        public Task<TDataClass> Get(int? id);
        public Task Update(TDataClass Buisiness);
        public Task Delete(int id);
    }
}
