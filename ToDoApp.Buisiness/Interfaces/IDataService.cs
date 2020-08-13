using System.Collections.Generic;

namespace TodoApp.Buisiness.Interfaces
{
    public interface IDataService<TDataClass>
    {
        public void Create(TDataClass Buisiness);
        public ICollection<TDataClass> GetAll();
        public TDataClass Get(int id);
        public void Update(TDataClass Buisiness);
        public void Delete(int id);
    }
}
