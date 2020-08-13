using System.Collections.Generic;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Data.Interfaces;

namespace TodoApp.Buisiness.Services
{
    public class InMemoryDataService<TDataClass> : IDataService<TDataClass> where TDataClass : TodoApp.Buisiness.Interfaces.IHasId
    {
        private readonly IDataProvider<TDataClass> _dataProvider;

        public InMemoryDataService(IDataProvider<TDataClass> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Create(TDataClass data)
        {
            _dataProvider.Create(data);
        }

        public void Delete(int id)
        {
            _dataProvider.Delete(id);
        }

        public TDataClass Get(int id)
        {
            return _dataProvider.Get(id);
        }

        public ICollection<TDataClass> GetAll()
        {
            return _dataProvider.GetAll();
        }

        public void Update(TDataClass data)
        {
            _dataProvider.Update(data);
        }
    }
}
