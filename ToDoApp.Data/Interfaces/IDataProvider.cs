using System.Collections.Generic;

namespace TodoApp.Data.Interfaces
{
    public interface IDataProvider<TDataClass>
    {
        public void Create(TDataClass data);
        public ICollection<TDataClass> GetAll();
        public TDataClass Get(int id);
        public void Update(TDataClass data);
        public void Delete(int id);
    }
}
