using System.Collections.Generic;

namespace TodoApp.Buisiness.Interfaces
{
    public interface IDataService<TDataClass>
    {
        public void Create(TDataClass data);
        public ICollection<TDataClass> GetAll();
        public TDataClass Get(int id);
        public void Update(TDataClass data);
        public void Delete(int id);
    }
}
